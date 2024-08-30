public class IsHandEmptyCondition : Condition {
    public override bool Validate(object param = null) {
        return BattleManager.instance.cardManager.GetHandCount() == 0;
    }

    public override Condition Clone() {
        return new IsHandEmptyCondition();
    }

}