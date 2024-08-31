using System;

[Serializable]
public class HexAbility32 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;

        BattleTask task = new HexOutTask(hex.tilePlaced, Enums.HexState.Excluded, p, hex);

        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility32();
    }
}

