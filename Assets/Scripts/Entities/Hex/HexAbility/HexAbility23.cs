using System;

[Serializable]
public class HexAbility23 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;

        BattleTask task = new HealTask(p, p, hex.stat.heal, hex);
        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility23();
    }
}
