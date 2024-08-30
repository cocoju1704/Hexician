using System;
using UnityEngine;

public class DamageTask : BattleTask {
    public Unit src;
    public Unit target;

    public Enums.DamageType damageType;
    protected int? amount;
    protected Hex srcHex;
    protected bool canApplyPower;
    protected bool canApplyAttackerRate;
    protected bool canApplyTargetRate;
    protected bool canIgnoreArmor;

    bool getsTargetFromRegister;

    public DamageTask(Unit src, Unit target, int? amount, Enums.DamageType damageType, Hex srcHex = null) {
        this.src = src;
        this.target = target;
        this.amount = amount;
        this.damageType = damageType;
        this.srcHex = srcHex;

        if(target == null) {
            getsTargetFromRegister = true;
        }

        if(damageType == Enums.DamageType.Normal) {
            canApplyPower = true;
            canApplyAttackerRate = true;
            canApplyTargetRate = true;
            canIgnoreArmor = false;
        }
        else if(damageType == Enums.DamageType.True || damageType == Enums.DamageType.Loss) {
            canApplyPower = false;
            canApplyAttackerRate = false;
            canApplyTargetRate = false;
            canIgnoreArmor = true;
        }
        else if(damageType == Enums.DamageType.Reflected) {
            canApplyPower = false;
            canApplyAttackerRate = false;
            canApplyTargetRate = false;
            canIgnoreArmor = false;
        }
    }

    public int ApplyPower(Unit src, int damage) {
        if(src == null) {
            return damage;
        }

        return damage + src.power;
    }

    public int ApplyAttackerRate(Unit src, int damage) {
        if(src == null) {
            return damage;
        }
        return damage;
    }

    public int ApplyTargetRate(Unit target, int damage) {
        if(target == null) {
            return damage;
        }
        
        return damage;
    }

    public int CalcuateDamage(int amount) {
        int damage = amount;

        // 파워 적용
        if(canApplyPower) {
            damage = ApplyPower(src, damage);
        }

        // 공격자 뎀증 계산
        if(canApplyAttackerRate) {
            damage = ApplyAttackerRate(src, damage);
        }

        // 피격자 뎀증 계산
        if(canApplyTargetRate) {
            damage = ApplyTargetRate(target, damage);
        }

        return damage;
    }

    protected Unit GetTarget() {
        if(!getsTargetFromRegister) {
            return target;
        }

        return register.target;
    }

    public override void Execute() {
        //??: if null이면 뒤에꺼로 대체
        target ??= GetTarget();
        amount ??= register.nums[0];
        int damage = CalcuateDamage((int) amount);
        register.nums[0] = damage;

        target.TakeDamage(src, damage, canIgnoreArmor);
    }

    protected override BattleTask CloneTask() {
        return new DamageTask(src, target, amount, damageType, srcHex);
    }
}