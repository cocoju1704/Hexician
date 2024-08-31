
using System;
using UnityEngine;

public abstract class HexAbility : Ability {
    protected Hex hex;
    public void SetHex(Hex hex) {
        this.hex = hex;
    }
}