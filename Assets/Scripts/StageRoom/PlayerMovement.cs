// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;
// using Unity.VisualScripting;
// using UnityEngine.UI;
// public class PlayerMovement : MonoBehaviour
// {
//     //변수
//     Vector3Int _coordinate;
//     Vector3Int _nextCoordinate;
//     public bool active;
//     [SerializeField] public StagePhase stagePhase;
//     public StageController stageController;

//     public Vector3Int coordinate {
//         get {
//             return _coordinate;
//         }
//         set {
//             _coordinate = value;
//         }
//     }
//     public Vector3Int nextCoordinate {
//         get {
//             return _nextCoordinate;
//         }
//         set {
//             _nextCoordinate = value;
//         }
//     }
//     public LegacyStageBoard stageBoard;
//     // Start is called before the first frame update

//     // Update is called once per frame
//     void Update()
//     {

//     }
//     public void InitPlayerMovement(Vector3Int coordinate, Vector3 pos, LegacyStageBoard stageBoard) {
//         transform.localPosition = pos;
//         this.coordinate = coordinate;
//         nextCoordinate = coordinate;
//         gameObject.name = "PlayerIcon";
//         this.stageBoard = stageBoard;
//     }
//     public void SetNextCoordinate(Vector3Int nextCoordinate) {
//         this.nextCoordinate = nextCoordinate;
//     }
//     public void MovePlayerTo() {
//         if (stagePhase.phase != Enums.StagePhase.PlayerAnimate) return;
//         Vector3 destPos = Utils.ConvertToPosition(nextCoordinate, stageBoard.stageTileSize);
//         DOTween.Sequence()
//             .Append(transform.DOMove(destPos, 1))
//             .OnComplete(() => {
//                 coordinate = nextCoordinate;
//                 //PlayerAnimate->EnterRoom
//                 stagePhase.ToNextPhase();
//                 stageController.ProceedOneTurn();
//             });
//     }
// }
