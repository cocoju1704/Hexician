using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility15 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();
        BattleBoard board = BattleManager.instance.board;

        Hex newHex = ResourceSystem.instance.MakeHex(hex.stat.magicNum1);

        List<BattleTask> tasks = new List<BattleTask>() {
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex),

            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            new FilterTilesTask(board.GetAllTiles(), new IsEmptyTileCondition()),
            new RandomTilesTask(tiles: null, count: null),
            new CreateHexTask( hex.tilePlaced, newHex, p)

        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility15();
    }
}
