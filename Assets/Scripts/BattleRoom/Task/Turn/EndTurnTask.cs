using DG.Tweening;
using UnityEngine;

public class EndTurnTask : BattleTask {
    public bool isPlayerTurn;

    public EndTurnTask(bool isPlayerTurn) {
        this.isPlayerTurn = isPlayerTurn;
    }

    public override void Execute() {
        BattleManager battleManager = BattleManager.instance;
        CardManager cardManager = BattleManager.instance.cardManager;

        if(isPlayerTurn) {
            Debug.Log("플레이어 턴 종료");

            battleManager.AddSeq(MyAnim.instance.GetClearHandAnim());

            cardManager.MoveAllHandToGraveyard();
            battleManager.MakePlayerCantContol();
        }
    }

    protected override BattleTask CloneTask() {
        return new EndTurnTask(isPlayerTurn);
    }
}