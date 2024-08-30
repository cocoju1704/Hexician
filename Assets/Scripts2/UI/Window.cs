using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Window : MonoBehaviour {
    public bool isOpened { get; private set; }
    public UnityEvent OnOpen = new UnityEvent();
    public UnityEvent OnClose = new UnityEvent();

    public void Open() {
        isOpened = true;
        gameObject.SetActive(true);

        OnOpen.Invoke();
    }

    public void Close() {
        isOpened = false;
        gameObject.SetActive(false);

        OnClose.Invoke();
    }
}
