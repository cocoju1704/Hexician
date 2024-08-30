using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility8 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new CheckConditionTask(new CompareHpCondition(Enums.ComparisonOperator.LE, p, 50)),

            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new MathOperationTask(Enums.MathOperator.Mul, idx: 0, hex.stat.damage),
            new BranchTask(true, new MathOperationTask(Enums.MathOperator.Mul, idx: 0, 2)),
            new DamageTask(p, target, null, Enums.DamageType.Normal, hex)
        };

        BattleManager.instance.AddTask(tasks, hex.register);

    }    

    public override HexAbility Clone() {
        return new HexAbility8();
    }
}
