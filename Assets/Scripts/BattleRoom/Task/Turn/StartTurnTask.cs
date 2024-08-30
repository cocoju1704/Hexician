using UnityEngine;
using DG.Tweening;
using TMPro;

public class StartTurnTask : BattleTask {
    public bool isPlayerTurn;
    public int drawCount;

    public StartTurnTask(bool isPlayerTurn) 
        : this(isPlayerTurn, 5) {}

    public StartTurnTask(bool isPlayerTurn, int drawCount) {
        this.isPlayerTurn = isPlayerTurn;
        this.drawCount = drawCount;
    }

    public override void Execute() {
        BattleManager battleManager = BattleManager.instance;

        if(isPlayerTurn) {
            Debug.Log("플레이어 턴 시작");
            for(int i = 0; i < drawCount; i++) {
                battleManager.AddTask(new DrawCardTask());
            }
            battleManager.AddTask(new PlayerCanControlTask());

            battleManager.AddSeq(MyAnim.instance.GetTurnStartAnim());
        }
        else {
            Debug.Log("몬스터 턴 시작");
            battleManager.StartMonstersAction();
        }
    }

    protected override BattleTask CloneTask() {
        return new StartTurnTask(isPlayerTurn);
    }
}