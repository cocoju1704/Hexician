using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
public class Chest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] Sprite openedSprite;
    [SerializeField] ObjectHolder holder;
    [SerializeField] Transform itemDestination;
    Item item;
    WorldObjectImage worldObjectImage;
    RawImage rawImage;
    SpriteRenderer spriteRenderer;
    Animator animator;
    [Header("Debug")]
    bool isOpened = false;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        Room room = GameManager.instance.GetCurrentRoom();
        List<int> itemIds = room.itemIds;
        List<Item> items = ResourceSystem.instance.MakeItem(itemIds);
        item = items[0];
        holder.SetObject(item.gameObject);
        // WorldObjectImage 설정
        worldObjectImage = holder.GetComponent<WorldObjectImage>();
        StartCoroutine(worldObjectImage.Render(item.gameObject));
        rawImage = GetComponentInChildren<RawImage>();
        rawImage.enabled = false;
        holder.GetComponent<Collider2D>().enabled = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        if(isOpened) {
            return;
        }

        animator.SetTrigger("EnterTrigger");
    }

    public void OnPointerExit(PointerEventData eventData) {
        if(isOpened) {
            return;
        }

        animator.SetTrigger("ExitTrigger");
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(isOpened) {
            return;
        }
        isOpened = true;
        animator.SetTrigger("ClickTrigger");
    }

    public void OpenChest() {
        spriteRenderer.sprite = openedSprite;
        spriteRenderer.sortingOrder = -1;
        rawImage.enabled = true;
        holder.transform.DOMove(itemDestination.position, 1f)
            .OnComplete(() => {
                holder.GetComponent<Collider2D>().enabled = true;
            });
        Tween sequence = DOTween.Sequence()
            .AppendInterval(2f)
            .Append(holder.transform.DOScale(Vector3.zero, 0.6f))
            .Join(holder.transform.DOMove(Constants.inventoryIconPosition, 1f))
            .OnComplete(() => {
                GetItem();
            });
    }
    public IEnumerator ShowItem() {
        yield return null;
    }
    public void GetItem() {
        item.GetComponent<IObtainable>().Obtain();
    }
}
