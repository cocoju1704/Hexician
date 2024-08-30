
using System;
using UnityEngine;

public abstract class HexAbility : Ability {
    protected Hex hex;
    protected Player p = BattleManager.instance.p;
    protected TaskManager tm = BattleManager.instance.taskManager;
    public void SetHex(Hex hex) {
        this.hex = hex;
    }
}