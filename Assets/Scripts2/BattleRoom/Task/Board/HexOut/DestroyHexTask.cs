public class DestroyHexTask : BattleTask {
    public Hex hex;
    
    public DestroyHexTask(Hex hex) {
        this.hex = hex;
    }
    
    public override void Execute() {
        hex.Destroy();
    }
    protected override BattleTask CloneTask() {
        return new DestroyHexTask(hex);
    }
}