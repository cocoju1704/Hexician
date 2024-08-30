using System;
using System.Collections.Generic;

[Serializable]
public class HexAbility13 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Excluded, p, hex),
            new MathOperationTask(Enums.MathOperator.Mul, 0, hex.stat.damage),
            new DamageAllTask(p, amount: null, Enums.DamageType.Normal, hex)
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility13();
    }
}
