using System.Collections.Generic;

public class CheckConditionTask : BattleTask {
    List<Condition> conditions;

    public CheckConditionTask(Condition condition) 
        : this(new List<Condition>(){ condition }) {}
    
    public CheckConditionTask(List<Condition> conditions) {
        this.conditions = conditions;
    }
  
    public override void Execute() {
        bool isOk = true;
        foreach(Condition condition in conditions) {
            if(!condition.Validate(register)) {
                isOk= false;
                break;
            }
        }

        register.flag = isOk;
    }

    protected override BattleTask CloneTask() {
        return new CheckConditionTask(conditions);
    }
}