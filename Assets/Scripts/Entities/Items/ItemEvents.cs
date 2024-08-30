using System;

using UnityEngine.Events;

// 아이템 효과 관련 이벤트를 모아놓은 곳
public class ItemEvents : Singleton<ItemEvents> {

    //첫 구매 관련
    public UnityEvent OnFirstPurchase;
    public UnityEvent OnShopCreate;
    // 아이템 획득 관련
    public UnityEvent<Item> OnItemObtain;
    public UnityEvent<int> OnLackOfGold;
    public int lackOfGold = 0;

}