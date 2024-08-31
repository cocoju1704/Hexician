using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility11 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        List<BattleTask> tasks = new List<BattleTask>() {
            new DamageTask(p, target, hex.stat.damage, Enums.DamageType.Normal, hex),

            new FilterTilesTask(hex.tilesInScope, new IsEmptyTileCondition()),
            new CountTilesTask(tiles: null, idxToStore: 0),
            new MathOperationTask(Enums.MathOperator.Mul, 0, hex.stat.magicNum1),
            new DamageTask(p, p, amount: null, Enums.DamageType.Loss, hex),

            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility11();
    }
}
