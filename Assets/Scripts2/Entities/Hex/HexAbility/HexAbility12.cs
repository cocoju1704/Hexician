using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility12 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new CheckConditionTask(new IsHandEmptyCondition()),
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex),
            new BranchTask(true, new DrawCardTask())
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility12();
    }
}
