using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility10 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;

        Hex newHex = ResourceSystem.instance.MakeHex(hex.stat.magicNum1);
        BattleTask task = new CreateHexTask(hex.tilePlaced, newHex, p);

        BattleManager.instance.AddTask(task, hex.register);
    }
    
    public override HexAbility Clone() {
        return new HexAbility10();
    }
}
