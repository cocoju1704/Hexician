using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility33 : HexAbility {
    
    public override void Execute() {
        BattleTask task = new ChangeHexTask(ResourceSystem.instance.MakeHex(hex.stat.magicNum1), hex.tilesInScope[0]);
        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility33();
    }
}

