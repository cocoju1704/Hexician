// 적 추격 2턴 딜레이
// 트리거: 적 추격 발동 시
using UnityEngine;
public class Item4 : Item {
    protected override void SetActions()
    {
        actions.Add(ItemActions.instance.AddPursuitDelay(2));
    }
    protected override void UseWhenObtained()
    {
        SetTriggers();
        actions[0]();
    }
    protected override void SetTriggers()
    {
        return;
    }
}