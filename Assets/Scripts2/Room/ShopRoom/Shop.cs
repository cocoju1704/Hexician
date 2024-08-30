using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    [Header("슬롯 프리팹")]
    public GameObject slotPrefab;
    [Header("클릭 방지 패널")]
    public CanvasGroup blockPanel;
    List<PurchasableSlot> slots = new();
    List<Item> items = new();
    List<Hex> hexes = new();
    bool _isFirstPurchase;
    public bool isFirstPurchase {
        get {
            return _isFirstPurchase;
        }
        set {
            if (!value) {
                ItemEvents.instance.OnFirstPurchase?.Invoke();
            }
            _isFirstPurchase = value;
        }
    }
    Player player = GameManager.instance.p;
    void Awake() {
        // 상점 관련 정보 초기화
        Room thisRoom = GameManager.instance.GetCurrentRoom();
        items = ResourceSystem.instance.MakeItem(thisRoom.itemIds);
        hexes  = ResourceSystem.instance.MakeHex(thisRoom.hexIds);
        CreateSlots();
        StartCoroutine(UpdateSlots());
        isFirstPurchase = true;
        // 클릭 방지 패널 비활성화
        blockPanel.blocksRaycasts = false;
    }
    void CreateSlots() {
        for (int i = 0; i < items.Count; i++) {
            GameObject container = GameObject.Find("SlotContainer");
            GameObject slotObject = Instantiate(slotPrefab, container.transform);
            slotObject.name = "PuchasableSlot " + i;
            PurchasableSlot slot = slotObject.GetComponent<PurchasableSlot>();
            slot.Init(items[i].gameObject, i);
            slots.Add(slot);
            slotObject.transform.localPosition = ShopUtils.CalculateShopItemPosition(1200, 600, Utils.GetSize(slotObject), 2, items.Count / 2, i);
        }
    }
    public IEnumerator UpdateSlots() {
        foreach (PurchasableSlot slot in slots) {
            slot.UpdateStatus(ApplyDiscount(slot.price));
        }
        yield return null;
    }

    public IEnumerator TryPurchase(int index) {
        blockPanel.blocksRaycasts = true;
        int slotPrice = slots[index].price;
        // 구매조건 확인
        // 돈 부족한지 확인
        if (player.gold < slotPrice) {
            int insufficientGold = slotPrice - player.gold;
            // 피로 골드 대체 가능한지 확인
            if (player.maxHp > insufficientGold) {
                ItemEvents.instance.OnLackOfGold?.Invoke(insufficientGold);
            }
            else {
                StartCoroutine(NotEnoughGoldEffect());
                yield return null;
            }
        }
        else {
            player.gold -= slotPrice;
        }
        // 구매 성공 시 이후 업데이트(플레이어 돈, 아이템)
        yield return StartCoroutine(slots[index].OnPurchaseEffect());
        yield return StartCoroutine(slots[index].DisableSlot());
        yield return StartCoroutine(ActualPurchase(index));
        yield return StartCoroutine(UpdateSlots());
    }
    public IEnumerator ActualPurchase(int index) {
        slots[index].itemOrHex.GetComponent<IPurchasable>().Purchase();
        blockPanel.blocksRaycasts = false;
        yield return null;

    }
    IEnumerator NotEnoughGoldEffect() {
        blockPanel.blocksRaycasts = false;
        yield return null;
    }

    // 아이템 관련 함수
    public int ApplyDiscount(int price) {
        // 할인 적용한 가격 계산
        int discountPrice = price;
        // 곱연산으로 할인 적용
        foreach (KeyValuePair<string, int> discount in player.discounts) {
            discountPrice = (int)(discountPrice * (1 - discount.Value / 100f));
        }
        return discountPrice;
    }
}