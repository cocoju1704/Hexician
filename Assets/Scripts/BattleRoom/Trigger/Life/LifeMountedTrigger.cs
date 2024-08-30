using System;
using UnityEngine;

[Serializable]
public class LifeMountedTrigger : Trigger {
    public LifeMountedTrigger() {}
    public LifeMountedTrigger(int count, bool canRepeat) 
        : base(1, false) {
        isLifeTrigger = true;
    }

    public override void Subscribe() {
        BattleManager.instance.taskManager.OnMountHex.RemoveListener(ReduceCount);
        BattleManager.instance.taskManager.OnMountHex.AddListener(ReduceCount);
    }

    public override void Unsubscribe() {
        BattleManager.instance.taskManager.OnMountHex.RemoveListener(ReduceCount);
    }

    protected override bool ValidateSelf(BattleTask task) {
        if(task is MountHexTask mcTask) {
            if(mcTask.hex == ownerHex) {
                return true;
            }
        }

        return false;
    }

    public override Trigger Clone() {
        return new LifeMountedTrigger(count, canRepeat);
    }
}