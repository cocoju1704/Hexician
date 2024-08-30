using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility31 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        BattleBoard board = BattleManager.instance.board;

        List<BattleTask> tasks = new List<BattleTask>() {
            new RepeatTask(hex.stat.magicNum1, new DiscardCardTask(card: null)),

            new FilterTilesTask(board.GetAllTiles(), new IsEmptyTileCondition()),
            new RandomTilesTask(tiles: null, hex.stat.magicNum2),
            new CreateHexTask(tiles: null, ResourceSystem.instance.MakeHex(hex.stat.magicNum3), p)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility31();
    }
}
