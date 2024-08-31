using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility30 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;

        List<BattleTask> tasks = new List<BattleTask>() {
            new FilterTilesTask(hex.tilesInScope, new IsEmptyTileCondition()),
            new CreateHexTask(tiles: null, ResourceSystem.instance.MakeHex(hex.stat.magicNum1), p)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility30();
    }
}
