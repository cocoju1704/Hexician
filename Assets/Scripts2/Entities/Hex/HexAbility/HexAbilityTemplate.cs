using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbilityTemplate : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        TaskManager tm = BattleManager.instance.taskManager;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {

        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbilityTemplate();
    }
}
