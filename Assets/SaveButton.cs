using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            SaveSystem.instance.SaveFile();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
