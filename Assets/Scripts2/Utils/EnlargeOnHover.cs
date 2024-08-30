using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
// 마우스 호버 시 확대 기능. Boxcollider2D 컴포넌트 필요
public class EnlargeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("설정")]
    public float scaleMultiplier = 1.25f;
    public float duration = 0.3f; 
    private Vector3 normalScale;
    private Vector3 enlargedScale;

    void Start()
    {
        normalScale = transform.localScale;
        enlargedScale = normalScale * scaleMultiplier;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(enlargedScale, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(normalScale, duration);
    }
}