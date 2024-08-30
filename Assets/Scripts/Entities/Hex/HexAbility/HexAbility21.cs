using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility21 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;

        List<BattleTask> tasks = new List<BattleTask>() {
            new CheckConditionTask(new CompareHpCondition(Enums.ComparisonOperator.LE, p, 25)),
            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new MathOperationTask(Enums.MathOperator.Mul, idx: 0, hex.stat.heal),

            new BranchTask(true, new MathOperationTask(Enums.MathOperator.Mul, idx: 0, constant: 2)),
            new HealTask(p, p, amount: null, hex)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility21();
    }
}
