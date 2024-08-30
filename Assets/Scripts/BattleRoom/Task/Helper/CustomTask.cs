using System;

public class CustomTask : BattleTask {
    Action func;
    public CustomTask(Action func) {
      this.func = func;
    }

    public override void Execute() {
        func?.Invoke();
    }

    protected override BattleTask CloneTask() {
        return new CustomTask(func);
    }
}