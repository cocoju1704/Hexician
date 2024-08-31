public class MathOperationTask : BattleTask {

    Enums.MathOperator op;
    int n1;
    int n2;
    int? n3;

    // nums[idx] = nums[idx] op constant
    public MathOperationTask(Enums.MathOperator op, int idx, int constant)
        : this(op, idx, constant, null) {}
    
    // nums[dest] = nums[idx1] op nums[idx2];
    public MathOperationTask(Enums.MathOperator op, int dest, int idx1, int? idx2) {
        this.op = op;
        this.n1 = dest;
        this.n2 = idx1;
        this.n3 = idx2;
    }

    public override void Execute() {
        int result;
        if(op == Enums.MathOperator.Assignment) {
            result = n2;
        }
        else if(n3 == null) {
            result = Utils.Cacluate(register.nums[n1], op, n2);
        }
        else {
            result = Utils.Cacluate(register.nums[n2], op, register.nums[(int) n3]);
        }

        register.nums[n1] = result;
    }

    protected override BattleTask CloneTask() {
        return new MathOperationTask(op, n1, n2, n3);
    }

}