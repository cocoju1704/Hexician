// 부정적인 아이템 획득 시 1회 차단.
using UnityEngine;
public class Item3 : Item {
    public void Condition(Item item) {
        if (item.itemClass == Enums.ItemClass.Negative) {
            if (count > 0) {
                actions[0]();
                count -= 1;
            }
            if (count == 0) {
                ItemEvents.instance.OnItemObtain.RemoveListener(Condition);
            }
        }
    }
    protected override void UseWhenObtained() {
        SetTriggers();
        count = 2;
        return;
    }
    protected override void SetActions() {
        actions.Add(ItemActions.instance.BlockNegativeItem());
    }
    protected override void SetTriggers() {
        ItemEvents.instance.OnItemObtain.AddListener(Condition);
        return;
    }

}