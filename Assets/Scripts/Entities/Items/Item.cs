using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour, IObtainable, IPurchasable {
    // 인터페이스 프로퍼티 구현 영역
    private Image _icon;
    public Image icon { get => _icon; set => _icon = value; }
    [HideInInspector] public int price;

    [Header("아이템 정보")]
    [HideInInspector] public int no;
    public new string name;
    public string description;
    public string flavourText;
    public int priceInput;
    public Enums.ItemClass itemClass;
    [HideInInspector] public int count;
    protected abstract void SetActions();
    protected abstract void SetTriggers();
    protected abstract void UseWhenObtained();
    public List<UnityAction> actions = new List<UnityAction>();
    public void Awake() {
        icon = GetComponent<Image>();
        price = priceInput;
        SetActions();
    }
    public void Obtain() {
        // Player.instance.itemIds.Add(itemNo);
        GameManager.instance.p.AddItem(this);
        UseWhenObtained();
    }
    public void Purchase() {
        Obtain();
    }
}
