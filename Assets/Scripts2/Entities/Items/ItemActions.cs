using System;
using UnityEngine;
using UnityEngine.Events;

// 아이템 효과 관련 이벤트를 모아놓은 곳
public class ItemActions : Singleton<ItemActions> {

    //아이템 후보 중 하나 선택해서 획득
    public UnityAction GetOneItemOutOfThree() {
        return () => {
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject itemChoiceUIPrefab = Resources.Load<GameObject>("Prefabs/UI/ItemChoiceUI");
            Instantiate(itemChoiceUIPrefab, canvas.transform);
        };
    }
    // 상점 할인율 추가
    public UnityAction AddDiscount(String itemName, int discountRate) {
        return () => {
            GameManager.instance.p.discounts.Add(itemName, discountRate);
        };
    }
    // 상점 특정 할인율 제거
    public UnityAction RemoveDiscount(String itemName) {
        return () => {
            GameManager.instance.p.discounts.Remove(itemName);
        };
    }
    public UnityAction BlockNegativeItem() {
        return () => {
            GameManager.instance.p.RemoveRecentItem();
        };
    }
    public UnityAction AddPursuitDelay(int delayTurns) {
        return () => {
            GameManager.instance.stage.enemyPursuitDelay += delayTurns;
        };
    }
    public UnityAction<int> UseBloodForGold() {
        return (int gold) => {
            if (gold < GameManager.instance.p.maxHp) {
                GameManager.instance.p.maxHp -= gold;
                GameManager.instance.p.gold = 0;
            }
        };
    }
}