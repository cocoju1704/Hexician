
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility0 : HexAbility {
    
    public override void Execute() {
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility0();
    }
}
