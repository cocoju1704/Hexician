public class RandomTargetTask : BattleTask {
    public override void Execute() {
        register.target = BattleManager.instance.GetRandomTarget();
    }
    protected override BattleTask CloneTask() {
        return new RandomTargetTask();
    }

}