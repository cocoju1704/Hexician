// using System;
// using UnityEngine;

// public class StagePhase : ScriptableObject{
//     public Action OnStagePhaseChanged;
//     Enums.StagePhase _phase = Enums.StagePhase.PlayerSelect;
//     public Enums.StagePhase phase {
//         get {
//             return _phase;
//         }
//         set {
//             _phase = value;
//         }
//     }
//     public void ToNextPhase() {
//         if (phase == Enums.StagePhase.ToNextTurn) {
//             phase = Enums.StagePhase.PlayerSelect;
//         }
//         else {
//             phase++;
//         }
//         OnStagePhaseChanged?.Invoke();
//         Debug.Log(phase);
//         //Debug.Log("Enums.StagePhase: " + phase.ToString());
//     }
//     public void InitStagePhase() {
//         phase = Enums.StagePhase.PlayerSelect;
//     }
// }
