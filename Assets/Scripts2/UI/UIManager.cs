using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UIManager : MonoBehaviour {

    List<Window> windows;

    void Awake() {
        windows = new List<Window>();
    }

    public void Toggle(GameObject windowObj) {
        Window window = windowObj.GetComponent<Window>();
        if(window == null) {
            return;
        }

        if(window.isOpened) {
            window.Close();
            windows.Remove(window);

            if(windows.Count != 0) {
                windows.Last().Open();
            }
        }
        else {
            if(windows.Count != 0) {
                windows.Last().Close();
            }

            window.Open();
            windows.Remove(window);
            windows.Add(window);
        }
    } 
}