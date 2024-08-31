using System.Collections.Generic;

public class RepeatTask : BattleTask {
    public int? count;
    public List<BattleTask> tasks;    
    
    public RepeatTask(int? count, BattleTask task) 
        : this(count, new List<BattleTask> { task }) {}

    public RepeatTask(int? count, List<BattleTask> tasks) {
        this.count = count;
        this.tasks = tasks;
    }

    public override void Execute() {
        count ??= register.nums[0];

        for(int i = 0; i < count; i++) {
            foreach(BattleTask task in tasks) {
                BattleManager.instance.AddTask(task, register);
            }
        }
    }

    protected override BattleTask CloneTask() {
        return new RepeatTask(count, tasks);
    }
}