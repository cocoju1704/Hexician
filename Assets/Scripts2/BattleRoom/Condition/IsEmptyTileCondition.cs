public class IsEmptyTileCondition : Condition {
    public override bool Validate(object param = null) {
        if(param is not BattleTile) {
            return false;
        }

        BattleTile tile = param as BattleTile;

        return tile.isEmpty;
    }
    public override Condition Clone() {
        return new IsEmptyTileCondition();
    }
}