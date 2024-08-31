using System.Collections.Generic;

public class DamageAllTask : DamageTask {
    public DamageAllTask(Unit src, int? amount, Enums.DamageType damageType, Hex srcHex = null) 
        : base(src, null, amount, damageType, srcHex) {
    }

    public override void Execute() {
        target ??= GetTarget();
        amount ??= register.nums[0];
        int damage = CalcuateDamage((int) amount);
        register.nums[0] = 0;
        
        List<Unit> targets = BattleManager.instance.GetAllTargets();
        foreach(Unit target in targets) {
            int currDamage = damage;
            
            if(damageType == Enums.DamageType.Normal) {
                currDamage = ApplyTargetRate(target, damage);
            }

            register.nums[0] += currDamage;
            target.TakeDamage(src, currDamage, canIgnoreArmor);
        }
    }

    protected override BattleTask CloneTask() {
        return new DamageAllTask(src, amount, damageType, srcHex);
    }
}