using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility16 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new MathOperationTask(Enums.MathOperator.Mul, 0, hex.stat.damage),
            new DamageTask(p, target, amount: null, Enums.DamageType.Normal),

            new CheckConditionTask(new CompareHpCondition(Enums.ComparisonOperator.LE, target, 0)),
            new BranchTask(true, new HealTask(p, p, hex.stat.heal))
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility16();
    }
}
