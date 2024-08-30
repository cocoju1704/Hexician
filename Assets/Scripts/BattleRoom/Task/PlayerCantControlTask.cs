using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerCantControlTask : BattleTask {
    public PlayerCantControlTask() {}

    public override void Execute() {
        BattleManager.instance.AddSeq(
            DOTween.Sequence()
                .AppendCallback(BattleManager.instance.MakePlayerCantContol)
                .Pause()
        );
    }

    protected override BattleTask CloneTask() {
        return new PlayerCantControlTask();
    }
}