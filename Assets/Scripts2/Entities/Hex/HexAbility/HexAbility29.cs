using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility29 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        BattleBoard board = BattleManager.instance.board;

        List<BattleTask> tasks = new List<BattleTask>() {
            new FilterTilesTask(board.GetAllTiles(), new IsEmptyTileCondition()),
            new RandomTilesTask(tiles: null, hex.stat.magicNum1),
            new CreateHexTask(tiles: null, ResourceSystem.instance.MakeHex(hex.stat.magicNum2), p)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility29();
    }
}
