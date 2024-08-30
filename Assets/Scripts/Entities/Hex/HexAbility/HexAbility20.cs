using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility20 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        BattleTask damageTask = new DamageTask(p, target, null, Enums.DamageType.Normal, hex);
        List<BattleTask> tasks = new List<BattleTask>() {
            new MathOperationTask(Enums.MathOperator.Assignment, idx: 1, constant: 0),
            new MathOperationTask(Enums.MathOperator.Assignment, idx: 2, constant: 0),

            new CheckConditionTask(new CompareHpCondition(Enums.ComparisonOperator.LE, p, 50)),
            new BranchTask(true, new MathOperationTask(Enums.MathOperator.Plus, idx: 1, constant: 1)),

            new CheckConditionTask(new CompareHpCondition(Enums.ComparisonOperator.LE, p, 25)),
            new BranchTask(true, new MathOperationTask(Enums.MathOperator.Plus, idx: 1, constant: 1)),

            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new MathOperationTask(Enums.MathOperator.Mul, 0, hex.stat.damage),
            damageTask,

            new MathOperationTask(Enums.MathOperator.Plus, dest: 0, idx1: 1, idx2: 2),
            new RepeatTask(count : null, damageTask)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility20();
    }
}
