using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility26 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new CheckConditionTask(new CompareValueCondition(Enums.ComparisonOperator.GE, idx: 0, value: 1)),
            new BranchTask(true, new DrawCardTask())
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility26();
    }
}
