using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility9 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        TaskManager tm = BattleManager.instance.taskManager;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new DamageTask(p, p, hex.stat.magicNum1, Enums.DamageType.Loss),
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex)
        };

        BattleManager.instance.AddTask(tasks);
    }

    public override HexAbility Clone() {
        return new HexAbility9();
    }
}
