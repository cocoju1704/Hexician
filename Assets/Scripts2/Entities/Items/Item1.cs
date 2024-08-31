// 첫 구매 아이템 반값 할인
// 즉 첫 구매 이전까지는 모든 아이템 반값 할인 가격이다가,
// 구매가 이루어지면 아이템이 비활성화 되어야 함
// 다음 상점에는 활성화가 되어야 함

using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Item1 : Item {
    protected override void UseWhenObtained()
    {
        SetTriggers();
    }
    protected override void SetActions()
    {
        actions.Add(ItemActions.instance.AddDiscount("Item 1", 50));
        actions.Add(ItemActions.instance.RemoveDiscount("Item 1"));
    }

    protected override void SetTriggers() {
        ItemEvents.instance.OnShopCreate.AddListener(actions[0]);
        ItemEvents.instance.OnFirstPurchase.AddListener(actions[1]);
    }
}