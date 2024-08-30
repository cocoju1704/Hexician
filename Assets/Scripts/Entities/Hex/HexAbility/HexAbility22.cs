using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility22 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        TaskManager tm = BattleManager.instance.taskManager;
        BattleBoard board = BattleManager.instance.board;

        List<BattleTask> tasks = new List<BattleTask>() {
            new DamageTask(p, p, hex.stat.damage, Enums.DamageType.Loss, hex),

            new FilterTilesTask(board.GetAllTiles(), new IsEmptyTileCondition()),
            new RandomTilesTask(tiles: null, hex.stat.magicNum1),
            new CreateHexTask(tiles: null, ResourceSystem.instance.MakeHex(hex.stat.magicNum1), p)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility22();
    }
}
