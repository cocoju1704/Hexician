using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using DG.Tweening;

public class StageBoard : MonoBehaviour {
    public Transform renderingStartPoint;
    [SerializeField] bool isInStageScene;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject playerIcon;

    Stage stage;
    Player p;
    Dictionary<Vector3Int, StageTile> tiles;

    List<Vector3Int> dirVectors = new List<Vector3Int>() { 
        Utils.Vectors[(int) Enums.Direction.TopLeft],
        Utils.Vectors[(int) Enums.Direction.Left],
        Utils.Vectors[(int) Enums.Direction.BottomLeft],
    };
    List<StageTile> tilesNearPlayer;

    bool canMove;
    public int enemyPursuitDelay = 0;

    void Awake() {
        tiles = new Dictionary<Vector3Int, StageTile>();
        canMove = isInStageScene;
    }

    void Start() {
        p = GameManager.instance.p;
        stage = GameManager.instance.stage;

        CreateBoard();
        CreatePlayerIcon();
        SetTilesNearPlayer();
        HighlightTilesNearPlayer();
    }

    void CreateBoard() {
        Dictionary<Vector3Int, Room> rooms = stage.GetAllRooms();

        foreach(KeyValuePair<Vector3Int, Room> pair in rooms) {
            CreateTile(pair.Key, pair.Value.roomType);
        }
    }

    void CreateTile(Vector3Int coordinate, Enums.RoomType roomType) {
        StageTile tile = Instantiate(tilePrefab, transform).GetComponent<StageTile>();
        tile.Init(this, coordinate, roomType);
        tiles.Add(coordinate, tile);
    }

    void CreatePlayerIcon() {
        playerIcon.transform.SetAsLastSibling();
        playerIcon.transform.position = tiles[p.coordinate].transform.position;
    }

    void SetTilesNearPlayer() {
        tilesNearPlayer = new List<StageTile>();
        
        foreach(Vector3Int dirVec in dirVectors) {
            Vector3Int nearCoordinate = p.coordinate + dirVec;

            if(tiles.ContainsKey(nearCoordinate)) {
                tilesNearPlayer.Add(tiles[nearCoordinate]);
            }
        }
    }
    
    void HighlightTilesNearPlayer() {
        foreach(StageTile tile in tilesNearPlayer) {
            tile.TurnOnHighlight();
        }
    }

    public void Move(Vector3Int destCoordinate) {
      if(!CanInteract(destCoordinate)) {
            return;
        }

        // 방문하려는 타일에 해당하는 Room에 방문처리
        Room room = stage.VisitRoom(destCoordinate);

        // 만약 모종의 이유로 방문 실패하면 처리 
        if(room == null) {
            return;
        }

        // 더이상 이동할 수 없게 만들고, 이동 애니메이션 후, 씬 전환
        Vector3 destPos = tiles[destCoordinate].transform.position;
        canMove = false;
        DOTween.Sequence()
            .Append(playerIcon.transform.DOMove(destPos, 1f))
            .AppendInterval(0.2f)
            .AppendCallback(() => {
                p.coordinate = destCoordinate;
                SceneLoadSystem.instance.LoadScene(room.roomType);
            });
    }

    public bool CanInteract(Vector3Int coordinate) {
        // 스테이지 씬이 아니면 무시
        if(!isInStageScene) {
            return false;
        }
        
        // 이미 움직였으면 무시
        if(!canMove) {
            return false;
        }

       // 방문하려는 Coordinate에 해당하는 타일이 없으면 실패
        StageTile tile;
        if(!tiles.TryGetValue(coordinate, out tile)) {
            return false;
        }

        // 방문하려는 타일이 플레이어와 인접하지 않으면 실패
        if(!tilesNearPlayer.Contains(tile)) {
            return false;
        }

        return true;
    }
}