using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour {
    public int maxHp;
    public int currentHp;
    public int armor;
    public int power;
    public UnityEvent OnChangeStatus = new UnityEvent();

    public void TakeDamage(Unit src, int amount, bool canIgnoreArmor = false) {
        if(canIgnoreArmor) {
            currentHp -= amount;
        }
        else {
            armor -= amount;

            if(armor < 0) {
                currentHp += armor;
                armor = 0;
            }
        }

        if(currentHp <= 0) {
            currentHp = 0;
            OnChangeStatus.Invoke();
        }
        else {
            OnChangeStatus.Invoke();
        }
    }

    public void ReduceHp(Unit src, int amount) {
        currentHp -= amount;
        OnChangeStatus.Invoke();
    }

    public void TakeHeal(Unit src, int amount) {
        currentHp += amount;
        if(currentHp > maxHp) {
            currentHp = maxHp;
        }

        OnChangeStatus.Invoke();
    }

    public void GainArmor(Unit src, int amount) {
        armor += amount;

        OnChangeStatus.Invoke();
    }

    public void AddPower(Unit src, int amount) {
        power += amount;

        power = Math.Max(power, 0);

        OnChangeStatus.Invoke();
    }

    public void IncrementMaxHp(Unit src, int amount) {
        maxHp += amount;
        currentHp += amount;

        OnChangeStatus.Invoke();
    }

    public int GetCurrentHpPercent() {
        float hpPercent = (float) currentHp / maxHp;
        return Mathf.RoundToInt(hpPercent * 100);
    }
}
