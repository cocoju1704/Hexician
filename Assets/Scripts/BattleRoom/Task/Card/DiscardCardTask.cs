using UnityEngine;

public class DiscardCardTask : BattleTask {
    public Card card;
    bool isRandomCard;

    public DiscardCardTask(Card card) {
        this.card = card;

        if(card == null) {
            isRandomCard = true;
        }
    }

    public override void Execute() {
        CardManager cardManager = BattleManager.instance.cardManager;
        
        if(isRandomCard) {
            this.card = register.card;
        }
        register.card = this.card;

        cardManager.MoveHandToGraveyard(card);

        BattleManager.instance.AddSeq(MyAnim.instance.GetVanishCardAnim(card));
    }

    protected override BattleTask CloneTask() {
        return new DiscardCardTask(card);
    }
}