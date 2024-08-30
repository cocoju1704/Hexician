using UnityEngine;
using UnityEngine.EventSystems;

public class FocusOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Vector3 enlargedScale;
    public float duration = 0.3f;
    private Vector3 normalScale;
    IFocusable focusable;
    void Start() {
        focusable = GetComponent<IFocusable>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        focusable.Focus();
    }

    public void OnPointerExit(PointerEventData eventData) {
        focusable.DeFocus();
    }
    
}