using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Turns : MonoBehaviour {
    [SerializeField] TextMeshProUGUI trunsTMP;
    [SerializeField] GameObject popup;
    [SerializeField] TextMeshProUGUI popupTMP;
    Stage stage;

    void Start() {
        stage = FindObjectOfType<Stage>();

        trunsTMP.text = stage.truns.ToString();
    }

    void OpenPopup() {
        popup.SetActive(true);
        popupTMP.text = stage.truns.ToString();

        Utils.WaitForSec(0.2f, ClosePopup);
    }

    void ClosePopup() {
        popup.SetActive(false);
    }
}