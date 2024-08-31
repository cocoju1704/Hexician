using UnityEngine;

public class DrawCardTask : BattleTask {
    public Card card;

    public override void Execute() {
        this.card = BattleManager.instance.cardManager.MoveDeckToHand();
        register.card = this.card; 

        BattleManager.instance.AddSeq(MyAnim.instance.GetAlignHandAnim(0.3f));
    }
    
    protected override BattleTask CloneTask() {
        return new DrawCardTask();
    }
}