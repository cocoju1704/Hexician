using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility14 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex),

            new HexOutTask(hex.tilesInScope, Enums.HexState.Excluded, p, hex),
            new IncrementMaxHpTask(p, p, amount: null)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility14();
    }
}
