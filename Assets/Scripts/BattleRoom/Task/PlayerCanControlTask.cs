using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerCanControlTask : BattleTask {
    public PlayerCanControlTask() {}

    public override void Execute() {
        BattleManager.instance.AddSeq(
            DOTween.Sequence()
                .AppendCallback(BattleManager.instance.MakePlayerCanContol)
                .Pause()
        );
    }

    protected override BattleTask CloneTask() {
        return new PlayerCanControlTask();
    }
}