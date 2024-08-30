using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleBoard : MonoBehaviour {
    public Indicator indicator;
    public Dictionary<Vector3Int, BattleTile> tiles;
    public float tileSize;

    [Header("필수 사전 설정")]
    [SerializeField]
    GameObject tilePrefab;

    public void Init(Dictionary<Vector3Int, bool> isActivated) {
        tiles = new Dictionary<Vector3Int, BattleTile>();
        tileSize = tilePrefab.transform.localScale.x * 3.01f;
      
        CreateTile(Vector3Int.zero);

        for(int radius = 1; radius <= 2; radius++) {
            Vector3Int currCoordinate = Vector3Int.zero + Utils.Vectors[(int) Enums.Direction.TopRight] * radius;

            for(int dir = 0; dir < 6; dir++) {

                for(int i = 0; i < radius; i++) {
                    currCoordinate += Utils.Vectors[dir];   

                    if(isActivated.ContainsKey(currCoordinate)) {
                        CreateTile(currCoordinate);
                    }
                }
            }
        }
    }

    public bool TryGetTile(Vector3Int coordinate, out BattleTile tile) {
        if(tiles.ContainsKey(coordinate)) {
            tile = tiles[coordinate];
            return true;
        }
        else {
            tile = null;
            return false;
        }
    }

    public List<BattleTile> GetAllTiles() {
        return tiles.Values.ToList();
    }

    void CreateTile(Vector3Int coordinate) {
        BattleTile tile = Instantiate(tilePrefab, transform).GetComponent<BattleTile>();
        tile.Init(coordinate);
        tiles.Add(coordinate, tile);
    }
}