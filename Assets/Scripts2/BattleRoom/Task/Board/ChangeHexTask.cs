public class ChangeHexTask : BattleTask {
    Hex newHex;
    BattleTile targetTile;


    public ChangeHexTask(BattleTile tileWithNewHex, BattleTile targetTile)
        : this(tileWithNewHex.hex, targetTile) {}
    public ChangeHexTask(Hex newHex, BattleTile targetTile) {
        this.newHex = newHex;
        this.targetTile = targetTile;
    }

    public override void Execute() {
        if(this.newHex == null) {
            return;
        }
        if(targetTile.isEmpty) {
            return;
        }

        Hex oldHex = targetTile.hex;
        oldHex.SetState(Enums.HexState.Destroyed).DeactivateTriggers();
        targetTile.DetachHex();

        Hex newHex = this.newHex.Clone();
        targetTile.AssignHex(newHex)
            .Operate();
    }

    protected override BattleTask CloneTask() {
        return new ChangeHexTask(newHex, targetTile);
    }
}