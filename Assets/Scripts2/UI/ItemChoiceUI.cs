using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Search;
using UnityEngine;

public class ItemChoiceUI : MonoBehaviour
{
    List<Item> items;
    List<Hex> hexes;
    [SerializeField] List<ItemChoiceSlot> slots;
    [SerializeField] GameObject itemChoiceSlotObject;
    [HideInInspector] public int choiceNum = 3;
    private Vector3 initialScale;

    void Awake()
    {
        // 정보 초기화
        Init(new List<int> { 1, 2, 3 });
        // 창 팝업 애니메이션
        PopUpAnim();
    }
    public void Init(List<int> itemIds) {
        slots = new List<ItemChoiceSlot>();
        items = ResourceSystem.instance.MakeItem(itemIds);
        for (int i = 0; i < items.Count; i++) {
            InitSlot(i);
        }
    }
    void PopUpAnim() {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(initialScale, 0.5f);
    }
    void InitSlot(int i) {
        // 아이템 인스턴스화 및 등록
        GameObject itemOrHex = items[i].gameObject;
        // 슬롯 인스턴스화 및 아이템 배정
        GameObject slotObject = Instantiate(itemChoiceSlotObject, transform);
        slotObject.name = "ItemChoiceSlot " + slots.Count;
        ItemChoiceSlot slot = slotObject.GetComponent<ItemChoiceSlot>();
        slot.Init(itemOrHex, slots.Count);
        slots.Add(slot);
        slotObject.transform.localPosition = ShopUtils.CalculateShopItemPosition(1200, 600, Utils.GetSize(slotObject), 1, items.Count, i);
    }
    public IEnumerator ChooseEffect(int selected){
        for (int i = 0; i < slots.Count; i++) {
            if (i == selected) {
                yield return StartCoroutine(slots[i].ChosenGraphicEffect());
            } else {
                yield return StartCoroutine(slots[i].NotChosenGraphicEffect());
            }
        }
        yield return AfterChooseEffect(selected);
    }
    public IEnumerator AfterChooseEffect(int selected) {
        items[selected].Obtain();
        DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, 0.3f))
            .OnComplete(() => Destroy(gameObject));
        yield return null;
    }
}
