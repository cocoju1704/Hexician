using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility6 : HexAbility {
    public override void Execute() {
        Hex newHex = ResourceSystem.instance.MakeHex(hex.stat.magicNum1);
        BattleTask task = new AddCardToDeckTask(newHex);
        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility6();
    }
}
