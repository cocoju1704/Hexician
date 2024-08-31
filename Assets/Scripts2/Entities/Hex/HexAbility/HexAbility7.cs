using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility7 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();
        BattleBoard board = BattleManager.instance.board;

        List<BattleTask> tasks = new List<BattleTask>() {
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex),
            new FilterTilesTask(board.GetAllTiles(), new IsEmptyTileCondition()),
            new RandomTilesTask(tiles: null, hex.stat.magicNum1),
            new CreateHexTask(tiles: null, ResourceSystem.instance.MakeHex(hex.stat.magicNum1), p)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility7();
    }
}
