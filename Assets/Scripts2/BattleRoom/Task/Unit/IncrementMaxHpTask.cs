public class IncrementMaxHpTask : BattleTask {
    public Unit src;
    public Unit target;
    public int? amount;

    public IncrementMaxHpTask(Unit src, Unit target, int? amount) {
        this.src = src;
        this.target = target;
        this.amount = amount;
    }

    public override void Execute() {
        amount ??= register.nums[0];

        target.IncrementMaxHp(src, (int) amount);
    }

    protected override BattleTask CloneTask() {
        return new IncrementMaxHpTask(src, target, amount);
    }
}