using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility4 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),

            new CheckConditionTask(new CompareValueCondition(Enums.ComparisonOperator.GE, idx: 0, hex.stat.magicNum1)),
            new BranchTask(true, new DamageAllTask(p, hex.stat.damage, Enums.DamageType.Normal, hex)),
            new BranchTask(false, new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex))
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }    

    public override HexAbility Clone() {
        return new HexAbility4();
    }
}
