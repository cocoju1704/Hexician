using UnityEngine;

public class StartBattleTask : BattleTask {
    public StartBattleTask() {}

    public override void Execute() {
        Debug.Log("배틀 시작");
        BattleManager.instance.AddSeq(MyAnim.instance.GetBattleStartAnim());
    }
    protected override BattleTask CloneTask() {
        return new StartBattleTask();
    }
}