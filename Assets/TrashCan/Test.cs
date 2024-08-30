using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            SaveSystem.instance.SaveFile();
        });
    }
}
