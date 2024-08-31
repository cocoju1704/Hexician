public class MountHexTask : BattleTask {
    public Unit src;
    public Hex hex;
    
    public MountHexTask(Hex hex) {
        this.src = BattleManager.instance.p;
        this.hex = hex;
    }

    public override void Execute() {
        hex.Operate();
    }

    protected override BattleTask CloneTask() {
        return new MountHexTask(hex);
    }
}