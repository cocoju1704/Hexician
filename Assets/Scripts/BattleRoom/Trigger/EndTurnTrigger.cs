using System;

[Serializable]
public class EndTurnTrigger : Trigger {

    public EndTurnTrigger() {}
    public EndTurnTrigger(int count, bool canRepeat)
        : base(count, canRepeat) {}

    public override void Subscribe() {
        BattleManager.instance.taskManager.OnEndTurn.RemoveListener(ReduceCount);
        BattleManager.instance.taskManager.OnEndTurn.AddListener(ReduceCount);
    }

    public override void Unsubscribe() {
        BattleManager.instance.taskManager.OnEndTurn.RemoveListener(ReduceCount);
    }

    protected override bool ValidateSelf(BattleTask task) {
        if(task is EndTurnTask endTurnTask) {
            return endTurnTask.isPlayerTurn;
        }
        
        return false;
    }

    public override Trigger Clone() {
        return new EndTurnTrigger(count, canRepeat);
    }

}