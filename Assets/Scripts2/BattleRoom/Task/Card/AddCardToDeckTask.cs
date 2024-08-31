public class AddCardToDeckTask : BattleTask {

    public Hex newHex;
    
    public AddCardToDeckTask(Hex newHex) {
        this.newHex = newHex;
    }
    
    public override void Execute() {
        CardManager cardManager = BattleManager.instance.cardManager;

        Card card = cardManager.MakeCard(newHex);
        cardManager.AddCardToDeck(card); 
    }

    protected override BattleTask CloneTask() {
        return new AddCardToDeckTask(newHex.Clone());
    }
}