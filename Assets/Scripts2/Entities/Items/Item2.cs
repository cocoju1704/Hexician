// 첫 구매 아이템 반값 할인
// 즉 첫 구매 이전까지는 모든 아이템 반값 할인 가격이다가,
// 구매가 이루어지면 아이템이 비활성화 되어야 함
// 다음 상점에는 활성화가 되어야 함
using System;

public class Item2 : Item {
    
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
        return;
    }
}