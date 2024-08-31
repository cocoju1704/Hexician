using System;
using UnityEngine;

[Serializable]
public class DismountHexTrigger : Trigger {
    public DismountHexTrigger() {}
    public DismountHexTrigger(int count, bool canRepeat) 
        : base(count, canRepeat) {}

    public override void Subscribe() {
        BattleManager.instance.taskManager.OnDismountHex.RemoveListener(ReduceCount);
        BattleManager.instance.taskManager.OnDismountHex.AddListener(ReduceCount);
    }

    public override void Unsubscribe() {
        BattleManager.instance.taskManager.OnDismountHex.RemoveListener(ReduceCount);
    }

    public override Trigger Clone() {
        return new DismountHexTrigger(count, canRepeat);
    }
}