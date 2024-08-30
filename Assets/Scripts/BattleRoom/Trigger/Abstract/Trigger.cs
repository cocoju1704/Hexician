using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public abstract class Trigger {
    [SerializeField]
    protected bool canRepeat;
    [SerializeField]
    protected int count;
    protected int resetCount;

    protected Condition condition;  
    protected Action func;

    public bool isLifeTrigger;
    
    protected Unit ownerUnit;
    protected Hex ownerHex;

    public Trigger() {}
    public Trigger(int count, bool canRepeat) {
        this.count = resetCount = count;
        this.canRepeat = canRepeat;

        this.condition = null;
        this.func = null;

        isLifeTrigger = false;

        this.ownerUnit = null;
        this.ownerHex = null;
    }

    public void Activate(Unit ownerUnit = null, Hex ownerHex = null) {
        this.ownerUnit = ownerUnit; // 트리거 소유 주체
        this.ownerHex = ownerHex; // 트리거 소유 코어
        
        count = resetCount;
        Subscribe();
    }

    public void Deactivate() {
        Unsubscribe();
    }

    public abstract void Subscribe();
    public abstract void Unsubscribe();
    

    // ValidateSelf vs Condition
    // ValidateSelf : 개별 트리거만의 고유한 condition을 적기 위한 부분
    // SetCondition : 반복적으로 사용되는 condition을 모듈화 하기 위한 부분
    // 즉 특이한 condition은 ValidateSelf에, 반복적인 condition은 SetCondition에 적는다.
    protected virtual bool ValidateSelf(BattleTask task) {
        return true;
    }
    public void SetCondition(Condition condition) {
        this.condition = condition;
    }
    
    public void SetFunc(Action func) {
        this.func = func;
    }

    protected void ReduceCount(BattleTask task) {
        if(!canRepeat && count == 0) {
            return;
        }

        if(!ValidateSelf(task)) {
            return;
        }

        if(!(condition?.Validate(task) ?? true)) {
            return;
        }

        count--;
        if(count == 0) {
            func?.Invoke();
            if(canRepeat) {
                count = resetCount;
            }
            else {
                Deactivate();
            }
        }
    }

    public abstract Trigger Clone();
}