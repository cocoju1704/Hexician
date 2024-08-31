using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility24 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Excluded, p, hex),
            new CheckConditionTask(new CompareValueCondition(Enums.ComparisonOperator.EQ, 0, 0)),
            new BranchTask(true, new HexOutTask(hex.tilePlaced, Enums.HexState.Excluded, p, hex))
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility24();
    }
}
