using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectHolder))]
public class Focus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] bool isRightPopup;
    [SerializeField] int padding;
    [SerializeField] GameObject popup;
    void Awake() {
        popup = Instantiate(popup, GameObject.Find("Canvas").transform);
        popup.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        popup.SetActive(true);
        GameObject hoveredObject = eventData.pointerEnter;
        ItemPopup itemPopup = popup.GetComponent<ItemPopup>();
        itemPopup.Init(hoveredObject.GetComponent<ObjectHolder>().obj.GetComponent<Item>());
        Vector2 popupPos = transform.position;
        
        if(transform is not RectTransform) {
            popupPos = Camera.main.WorldToScreenPoint(transform.position);
        }

        if(isRightPopup) {
            popupPos.x += padding;
        }
        else {
            popupPos.x -= padding;
        }

        popup.transform.position = popupPos;
        // holder.obj.GetComponent<IFocusable>()?.Focus();
    }

    public void OnPointerExit(PointerEventData eventData) {
        popup.SetActive(false);
    }

    public void OnPointerExit() {

    }


}
