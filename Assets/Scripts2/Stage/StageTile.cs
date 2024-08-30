using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] Image roomIconImage;
    [SerializeField] Image borderImage;

    float tileSize;
    Vector3Int coordinate;
    StageBoard board;
    Enums.RoomType roomType;
    Tween bouncingTween;

    void Awake() {
        RectTransform rect = GetComponent<RectTransform>();
        tileSize = GetComponent<RectTransform>().sizeDelta.y / 2;

        rect.sizeDelta = new Vector2(tileSize * Mathf.Sqrt(3), tileSize * 2);
    }

    public void Init(StageBoard board, Vector3Int coordinate, Enums.RoomType roomType) {
        this.board = board;
        this.coordinate = coordinate;
        transform.position = Utils.ConvertToPosition(this.coordinate, tileSize) + board.renderingStartPoint.position;
        name = coordinate.ToString();

        this.roomType = roomType;
    }

    public void ChangeRoomType(Enums.RoomType roomType) {
        this.roomType = roomType;
        GetComponent<Renderer>().material.color = new Color(255, 255, 0);
    }  

    public void TurnOnHighlight() {
        Color color = borderImage.color;
        color.a = 255;
        borderImage.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(!board.CanInteract(coordinate)) {
            return;
        }

        bouncingTween = transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.4f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void OnPointerExit(PointerEventData eventData) {
        bouncingTween?.Pause();
        if(transform.localScale != Vector3.one) {
            transform.DOScale(Vector3.one, 0.1f);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(!board.CanInteract(coordinate)) {
            return;
        }
        
        board.Move(coordinate);
    }
}
