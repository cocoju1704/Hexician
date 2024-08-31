// 랜덤 아이템 3개 중 하나 획득
using UnityEngine;
public class Item5 : Item {
    protected override void SetActions()
    {
        actions.Add(ItemActions.instance.GetOneItemOutOfThree());
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