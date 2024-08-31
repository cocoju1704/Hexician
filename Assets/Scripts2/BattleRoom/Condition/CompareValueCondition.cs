using System;

[Serializable]
public class CompareValueCondition : Condition {
    Enums.ComparisonOperator op;
    int idx;
    int value;
    public CompareValueCondition() {
        op = Enums.ComparisonOperator.EQ;
        idx = 0;
        value = 0;
    }
    public CompareValueCondition(Enums.ComparisonOperator op, int idx, int value) {
        this.op = op;
        this.idx = idx;
        this.value = value;
    }

    public override bool Validate(object param = null) {
        if(param is not Register) {
            return false;
        }

        Register register = param as Register;

        int num = register.nums[idx];

        return Utils.Compare(num, op, value);
    }



    public override Condition Clone() {
        return new CompareValueCondition(op, idx, value);
    }
}