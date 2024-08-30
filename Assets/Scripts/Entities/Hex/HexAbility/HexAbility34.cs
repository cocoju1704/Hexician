using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility34 : HexAbility {
    
    public override void Execute() {
        BattleTask task = new ChangeHexTask(hex.tilesInScope[0].hex, hex.tilePlaced);
        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility34();
    }
}

