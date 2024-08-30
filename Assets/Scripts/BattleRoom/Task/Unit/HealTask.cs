public class HealTask : BattleTask {
    public Unit src;
    public Hex srcHex;
    public Unit target;
    public int? amount;

    public HealTask(Unit src, Unit target, int? amount, Hex srcHex = null) {
        this.src = src;
        this.target = target;
        this.amount = amount;
        this.srcHex = srcHex;
    }

    public override void Execute() {
        amount ??= register.nums[0];

        target.TakeHeal(src, (int) amount);
    }

    protected override BattleTask CloneTask() {
        return new HealTask(src, target, amount, srcHex);
    }
}