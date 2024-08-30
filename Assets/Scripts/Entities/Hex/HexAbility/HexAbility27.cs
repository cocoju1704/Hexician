using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility27 : HexAbility {
    public override void Execute() {
        int count = BattleManager.instance.cardManager.GetHandCount();

        for(int i = 0; i < count; i++) {
            Card card = BattleManager.instance.cardManager.GetCardInHand(i);
            BattleManager.instance.AddTask(new DiscardCardTask(card), hex.register);
        }

        for(int i = 0; i < count; i++) {
            BattleManager.instance.AddTask(new DrawCardTask());
        }
    }

    public override HexAbility Clone() {
        return new HexAbility27();
    }
}
