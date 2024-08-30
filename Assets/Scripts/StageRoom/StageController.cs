// using UnityEngine;
// using UnityEngine.Tilemaps;
// using UnityEngine.SceneManagement;
// using UnityEditor.Build.Player;
// using System.Linq.Expressions;
// using DG.Tweening;

// //스테이지 조작 관련
// public class StageController : MonoBehaviour {
//     public GameObject playerPrefab;
//     //변수
//     [Header("Prefabs")]
//     [SerializeField] public StagePhase stagePhase;
//     [SerializeField] public GameObject stageBoardPrefabs;
//     [SerializeField] public LegacyStageTile highlightedTile;
//     [SerializeField] public Camera mainCamera;

//     PlayerMovement playerMovement;
//     LegacyStageBoard stageBoard;
//     LayerMask mask;
//     [HideInInspector]
//     public int turns;
//     [Header("Zoom In Settings")]
//     public float zoomDuration = 0.5f; // Duration of the zoom in seconds
//     public float targetOrthographicSize = 4f; // Desired orthographic size for the zoom

//     private Vector3 originalPosition; // Store the original position of the camera
//     private float originalOrthographicSize; // Store the original orthographic size of the camera
//     void Awake() {
//         turns = 1;
//         mask = LayerMask.GetMask("StageTile");
//         Debug.Log(stageBoardPrefabs);
//         stageBoard = stageBoardPrefabs.GetComponent<LegacyStageBoard>();
//         playerMovement = stageBoard.playerPrefab.GetComponent<PlayerMovement>();
//         stagePhase.InitStagePhase();
//         originalPosition = mainCamera.transform.position;
//         originalOrthographicSize = mainCamera.orthographicSize;
//     }
    
//     void Update() {
//         HighlightNextMove();
//         if(Input.GetMouseButtonDown(0)) {
//             SelectNextTile();
//         }
//     }
//     // 기본 입력값 관련 함수
//     public LegacyStageTile GetMouseOverTile() {
//         Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         Ray2D ray = new Ray2D(pos, Vector2.zero);
//         RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, mask);
        
//         if(!hit) {
//             return null;
//         }
//         // 뒤에 보드 타일이 있다면 저장해두고 위치를 고정
//         LegacyStageTile mouseOverTile =  hit.collider.gameObject.GetComponent<LegacyStageTile>();
//         return mouseOverTile;
//     }
//     public void HighlightNextMove(){
//         if (stagePhase.phase != Enums.StagePhase.PlayerSelect) return;
//         LegacyStageTile mouseOverTile =  GetMouseOverTile();
//         if (mouseOverTile == null) {
//             if (highlightedTile != null) highlightedTile.TurnOffHighlight();
//             return;
//         }
//         if (Utils.IsAdjacentFront(mouseOverTile.coordinate, GetPlayerTile().coordinate)) {
//             if (highlightedTile != null) highlightedTile.TurnOffHighlight();
//             highlightedTile =  mouseOverTile;
//             highlightedTile.TurnOnHighlight();
//         }
//     }
//     // 턴 관련 함수
//     public void SelectNextTile() {
//         if (stagePhase.phase != Enums.StagePhase.PlayerSelect) return;
//         LegacyStageTile clickedTile = GetMouseOverTile();
//         if (clickedTile == null) {
//             return;
//         }
//         if (!Utils.IsAdjacent(clickedTile, GetPlayerTile())) {
//             return;
//         }
//         stagePhase.ToNextPhase();// PlayerSelect->PlayerAnimate
//         playerMovement.SetNextCoordinate(clickedTile.coordinate);
//         playerMovement.MovePlayerTo();
//     }
//     public void ProceedOneTurn() {
//         ToRoom(); //EnterRoom -> InRoom ->ExitRoom
//         TurnOnCam(); //ExitRoom -> DisableTiles
//         EnemyPursuit(); // DisableTiles -> ToNextTurn
//         IncrementTurn(); // ToNextTurn -> PlayerSelect
//     }
//     public void ToRoom(){
//         LegacyStageTile tile = GetPlayerTile();
//         if (stagePhase.phase != Enums.StagePhase.EnterRoom) return;
//         if (tile.roomType == Enums.RoomType.Shop) {
//             SceneManager.LoadSceneAsync("ShopScene");
//         }
//         if (tile.roomType == Enums.RoomType.Chest) {
//             SceneManager.LoadSceneAsync("TreasureScene");
//         }
//         //임시 EnterRoom->ExitRoom
//         stagePhase.ToNextPhase();
//         stagePhase.ToNextPhase();
//         ZoomInOnTarget();
//         //ResetCamera();
//     }
//     public void TurnOnCam() {
//         if (stagePhase.phase != Enums.StagePhase.ExitRoom) return;
//         Camera.main.gameObject.SetActive(true);
//         stagePhase.ToNextPhase(); //ExitRoom->DisableTiles
//     }
//     void EnemyPursuit() {
//         if (stagePhase.phase != Enums.StagePhase.DisableTiles) return;
//         stageBoard.DisableTiles(turns - 2- ItemVariables.instance.pursuitDelay);
//         stagePhase.ToNextPhase(); //DisableTiles->ToNextTurn
//     }
//     public void IncrementTurn() {
//         if (stagePhase.phase != Enums.StagePhase.ToNextTurn) return;
//         turns++;
//         stageBoard.UpdateTurnUI();
//         stagePhase.ToNextPhase(); //ToNextTurn->PlayerSelect
//     }
//     public LegacyStageTile GetPlayerTile() {
//         return stageBoard.tiles[playerMovement.coordinate];
//     }


//     public void ZoomInOnTarget()
//     {
//         // Calculate the new position for the camera to focus on the target
//         Debug.Log(GetPlayerTile().transform.position);
//         Vector3 targetPosition = GetPlayerTile().transform.position;
//         Vector3 direction = (targetPosition - mainCamera.transform.position).normalized;
//         float distance = Vector3.Distance(mainCamera.transform.position, targetPosition); // Adjust the distance factor as needed
//         DOTween.Sequence()
//             .OnStart(() => {
//                 mainCamera.transform.DOMove(targetPosition - direction, zoomDuration);
//                 mainCamera.DOOrthoSize(targetOrthographicSize, zoomDuration);
//             })
//             .AppendInterval(1f)
//             .OnComplete(() => {
//                 ResetCamera();
//             });
//         // Tween the camera position and field of view

//     }

//     public void ResetCamera()
//     {
//         // Reset the camera position and field of view to the original values
//         mainCamera.transform.DOMove(originalPosition, zoomDuration);
//         mainCamera.DOOrthoSize(originalOrthographicSize, zoomDuration);
//     }
// }
