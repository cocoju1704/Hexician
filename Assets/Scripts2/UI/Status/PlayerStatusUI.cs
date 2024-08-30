using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : StatusUI {
    [SerializeField] TextMeshProUGUI goldTMP;

    void Start() {
        Player p = FindObjectOfType<Player>();
        Init(p);
        UpdateUI();
    }

    protected override void UpdateUI() {
        base.UpdateUI();

        Player p = unit as Player; 
        goldTMP.text = p.gold.ToString();
    }
}
