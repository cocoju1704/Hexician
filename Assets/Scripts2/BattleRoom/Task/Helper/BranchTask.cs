using System.Collections.Generic;

public class BranchTask : BattleTask {
    public bool branchFlag;
    public List<BattleTask> tasks;

    public BranchTask(bool branchFlag, BattleTask task)
        : this(branchFlag, new List<BattleTask> { task }) {}
        
    public BranchTask(bool branchFlag, List<BattleTask> tasks) {
        this.branchFlag = branchFlag;
        this.tasks = tasks;
    }
    
    public override void Execute() {
        if(branchFlag != register.flag) {
            return;
        }
        
        BattleManager.instance.AddTask(tasks, register);
    }

    protected override BattleTask CloneTask() {
        return new BranchTask(branchFlag, tasks);
    }
}