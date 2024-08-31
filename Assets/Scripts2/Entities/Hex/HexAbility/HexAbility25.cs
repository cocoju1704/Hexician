using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility25 : HexAbility {
    
    public override void Execute() {
        BattleTask task = new DrawCardTask();

        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility25();
    }
}
