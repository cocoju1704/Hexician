using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatusUI : StatusUI {
    void Start() {
        Monster p = FindObjectOfType<Monster>();
        Init(p);
        UpdateUI();
    }

    protected override void UpdateUI() {
        base.UpdateUI();
    }
}
