using System;
using UnityEngine;

[Serializable]
public class AttackedTrigger : Trigger {
    [SerializeField]
    Enums.DamageType damageType;

    public AttackedTrigger() {}
    public AttackedTrigger(int count, bool canRepeat)
        : base(count, canRepeat) {}
    public AttackedTrigger(int count, bool canRepeat, Enums.DamageType damageType) 
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
        if(task is DamageAllTask damageAllTask) {
            if(ownerUnit == damageAllTask.src) {
                return false;
            }
            else {
                return true;
            }
        }

        if(task is DamageTask damageTask) {
            if(ownerUnit == damageTask.target) {
                return true;
            }
            else {
                return false;
            }
        }

        return false;
    }

    public override Trigger Clone() {
        return new AttackedTrigger(count, canRepeat, damageType);
    }
}