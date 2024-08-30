using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopKeeper2 : MonoBehaviour, IPointerClickHandler
{
    // 변수
    [SerializeField] Canvas canvas;
    //함수
    void Start()
    {
        canvas.enabled =false;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StartCoroutine(CloseShop());
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        canvas.enabled = true;
    }
    IEnumerator CloseShop() {
        canvas.enabled = false;
        yield return null;
    }

}