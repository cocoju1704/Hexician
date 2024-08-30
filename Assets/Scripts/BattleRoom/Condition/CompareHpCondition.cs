using UnityEngine;

public class CompareHpCondition : Condition {
    Enums.ComparisonOperator op;
    Unit target;
    int hpPercent;

    public CompareHpCondition(Enums.ComparisonOperator op, Unit target, int hpPercent) {
        this.op = op;
        this.target = target;
        this.hpPercent = hpPercent;
    }
    public override bool Validate(object param = null) {
        int currHpPercent = target.currentHp * 100  / target.maxHp;
        Debug.Log(currHpPercent);
        return Utils.Compare(currHpPercent, op, hpPercent); 
    }

    public override Condition Clone() {
        return new CompareHpCondition(op, target, hpPercent);
    }
} 
    
    