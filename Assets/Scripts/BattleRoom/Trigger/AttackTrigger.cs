using System;
using UnityEngine;

[Serializable]
public class AttackTrigger : Trigger {
    [SerializeField]
    Enums.DamageType damageType;

    public AttackTrigger() {}
    public AttackTrigger(int count, bool canRepeat)
        : base(count, canRepeat) {}
    public AttackTrigger(int count, bool canRepeat, Enums.DamageType damageType) 
        : base(count, canRepeat) {
        this.damageType = damageType;
    }


    public override void Subscribe() {
        BattleManager.instance.taskManager.OnAttack.RemoveListener(ReduceCount);
        BattleManager.instance.taskManager.OnAttack.AddListener(ReduceCount);
    }

    public override void Unsubscribe() {
        BattleManager.instance.taskManager.OnAttack.RemoveListener(ReduceCount);
    }

    protected override bool ValidateSelf(BattleTask task) {
        if(task is DamageTask damageTask) {
            if(damageTask.src == ownerUnit && damageTask.damageType == damageType) {
                return true;
            }
            else {
                return false;
            }
        }

        return false;
    }

    public override Trigger Clone() {
        return new AttackTrigger(count, canRepeat, damageType);
    }
}