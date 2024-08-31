using System;

[Serializable]
public class StartTurnTrigger : Trigger {

    public StartTurnTrigger() {}
    public StartTurnTrigger(int count, bool canRepeat)
        : base(count, canRepeat) {}

    public override void Subscribe() {
        BattleManager.instance.taskManager.OnEndTurn.RemoveListener(ReduceCount);
        BattleManager.instance.taskManager.OnEndTurn.AddListener(ReduceCount);
    }

    public override void Unsubscribe() {
        BattleManager.instance.taskManager.OnEndTurn.RemoveListener(ReduceCount);
    }

    protected override bool ValidateSelf(BattleTask task) {
        if(task is StartTurnTask startTurnTas) {
            return startTurnTas.isPlayerTurn;
        }
        
        return false;
    }

    public override Trigger Clone() {
        return new StartTurnTrigger(count, canRepeat);
    }

}