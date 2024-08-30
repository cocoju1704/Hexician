public abstract class BattleTask {
    Register _register = null;
    protected Register register {
        get {
            if(_register == null) {
                _register = BattleManager.instance.taskManager.register;
            }
            return _register;
        }
    }

    public abstract void Execute();
    public BattleTask Clone() {
        return CloneTask()
            .SetRegister(register);
    } 

    protected abstract BattleTask CloneTask();


    public BattleTask SetRegister(Register register) {
        _register = register;
        return this;
    }
}