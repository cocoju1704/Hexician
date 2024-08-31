// 피로 골드 대체 가능
using System;

// 피로 골드 대체 가능
using UnityEngine;
using UnityEngine.Events;
public class Item6 : Item {
    protected override void SetActions()
    {
        return;
    }
    protected override void UseWhenObtained()
    {
        SetTriggers();
    }
    protected override void SetTriggers()
    {
        ItemEvents.instance.OnLackOfGold.AddListener(ItemActions.instance.UseBloodForGold());
        return;
    }


}