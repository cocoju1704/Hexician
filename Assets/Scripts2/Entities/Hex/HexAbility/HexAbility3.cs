using System;
using System.Collections.Generic;

[Serializable]
public class HexAbility3 : HexAbility {
    public override void Execute() {
        Player p = BattleManager.instance.p;
        TaskManager tm = BattleManager.instance.taskManager;

        List<BattleTask> tasks = new List<BattleTask>() {
            new HexOutTask(hex.tilesInScope, Enums.HexState.Dismounted, p, hex),
            
            new MathOperationTask(Enums.MathOperator.Mul, 0, hex.stat.damage),

            new RepeatTask(count: hex.stat.magicNum1, new List<BattleTask>() {
                new RandomTargetTask(),
                new DamageTask(p, target: null, amount: null, Enums.DamageType.Normal, hex)
            })
        };

        BattleManager.instance.AddTask(tasks, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility3();
    }
}