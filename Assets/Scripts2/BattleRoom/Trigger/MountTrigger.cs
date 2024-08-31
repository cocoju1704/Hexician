using System;

[Serializable]
public class MountTrigger : Trigger {

    public MountTrigger() {}
    public MountTrigger(int count, bool canRepeat)
        : base(count, canRepeat) {}

    public override void Subscribe() {
        return;
    }

    public override void Unsubscribe() {
        return;
    }

    protected override bool ValidateSelf(BattleTask task) {
        return true;
    }

    public override Trigger Clone() {
        return new MountTrigger(count, canRepeat);
    }

}