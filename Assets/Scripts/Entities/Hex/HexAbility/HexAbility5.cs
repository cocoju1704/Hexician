using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility5 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        BattleTask task = new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal);
        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility5();
    }
}
