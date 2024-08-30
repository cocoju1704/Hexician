using System;

[Serializable]
public class LifeExcludedTrigger : Trigger {
    public LifeExcludedTrigger() {}
    public LifeExcludedTrigger(int count, bool canRepeat)
        : base(1, false) {
        isLifeTrigger = true;

    }
        
    public override void Subscribe() {
        BattleManager.instance.taskManager.OnDestroyHex.RemoveListener(ReduceCount);
        BattleManager.instance.taskManager.OnDestroyHex.AddListener(ReduceCount);
    }

    public override void Unsubscribe() {
        BattleManager.instance.taskManager.OnDestroyHex.RemoveListener(ReduceCount);
    }
    protected override bool ValidateSelf(BattleTask task) {
        if(task is DestroyHexTask dcTask) {
            if(dcTask.hex == ownerHex && ownerHex.state == Enums.HexState.Excluded) {
                return true;
            }
        }

        return false;
    }
    public override Trigger Clone() {
        return new LifeExcludedTrigger(count, canRepeat);
    }
}