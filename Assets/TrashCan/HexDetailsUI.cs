using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HexDetailsUI : MonoBehaviour {
    [Header("필수 사전 설정")]
    [SerializeField]
    TextMeshProUGUI nameTMP;

    [SerializeField]
    TextMeshProUGUI descriptionTMP;
    [SerializeField]
    Image image;

    Hex hex;

    public void Set(Hex hex) {
        this.hex = hex;
        
        image.sprite = hex.sprite;
        nameTMP.text = hex.name;
        descriptionTMP.text = hex.description;

        // hex.OnSimulate.AddListener(ChangeDescriptions);
    }

    public void Clear() {
        // hex.OnSimulate.RemoveListener(ChangeDescriptions);
    }

    void ChangeDescriptions(string description) {
        descriptionTMP.text = description;
    }
}
