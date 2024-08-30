using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HexAbility28 : HexAbility {
    
    public override void Execute() {
        Player p = BattleManager.instance.p;
        Unit target = BattleManager.instance.GetTarget();

        BattleTask task = new RepeatTask(count: hex.stat.magicNum1, new List<BattleTask> {
            new DrawCardTask(),

            new CustomTask(() => {
                Card drawnCard = hex.register.card;
                Hex drawnHex = drawnCard.hex;
                EndTurnTrigger trigger = new EndTurnTrigger();

                Action triggered = () => {
                    BattleTask excludingTask = new CustomTask(() => {
                        BattleManager.instance.cardManager.MoveGraveyardToVoid(drawnCard);
                    });
                    BattleManager.instance.AddTask(excludingTask, drawnHex.register);
                    
                    drawnHex.RemoveTrigger(trigger);
                };

                trigger.SetFunc(triggered);
                drawnHex.AddTrigger(trigger);
            })

        });

        BattleManager.instance.AddTask(task, hex.register);
    }

    public override HexAbility Clone() {
        return new HexAbility28();
    }
}
