using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using System;
using System.Linq;

public class Card : MonoBehaviour {
    [Header ("필수 사전 설정")]
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public TextMeshPro nameTMP;
    [SerializeField]
    GameObject border;
    
    public Hex hex;
    public BattleTile tilePlaced;

    Tweener moveTween;
    Tweener rotateTween;
    Tweener scaleTween;
    Vector3 positionInHand;
    Vector3 rotationInHand;
    Vector3 scaleInHand;
    int orderInHand;

    new Collider2D collider;

    public void Init(Hex hex) {
        SetHex(hex);
        
        tilePlaced = null;

        scaleInHand = new Vector3(0.6f, 0.6f, 1);
        moveTween = transform.DOMove(transform.position, 0.1f).SetAutoKill(false);
        rotateTween = transform.DORotate(Vector3.zero, 0.1f).SetAutoKill(false);
        scaleTween = transform.DOScale(scaleInHand, 0.1f).SetAutoKill(false);

        collider = GetComponent<Collider2D>();
    }

    public void SetHex(Hex hex) {
        this.hex = hex;

        nameTMP.text = hex.name;
        spriteRenderer.sprite = hex.sprite;
    }

    void OnDestroy() {
        moveTween.Kill();
        rotateTween.Kill();
        scaleTween.Kill();
    }

    public void Focus() {
        rotateTween.ChangeValues(transform.rotation.eulerAngles, Vector3.zero, 0.1f).Restart();
        scaleTween.ChangeEndValue(scaleInHand + new Vector3(0.3f, 0.3f, 0), 0.1f).Restart();

        ChangeSortingOrder(1000);
    }

    public void StartDragForMove(Vector3 scale) {
        moveTween.Pause();
        rotateTween.ChangeValues(transform.rotation.eulerAngles, Vector3.zero, 0.1f).Restart();
        scaleTween.ChangeEndValue(scale, 0.4f).Restart();
    }

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }

    public void PlaceTemp(BattleTile tile) {
        transform.position = new Vector3(-100, -100, 1);
        tilePlaced = tile;
        tile.SetCard(this);
        tile.AssignHex(hex);
    }

    public void  Place() {
        BattleManager.instance.AddTask(new List<BattleTask>() {
            new PlayCardTask(this, tilePlaced),
            new MountHexTask(hex)
        });
    }

    public void ChangeTiles(Enums.Direction dir) {
        hex.ChangeTiles(dir);
    }

    public void Cancel() {
        if(tilePlaced != null) {
            transform.position = tilePlaced.transform.position;
            tilePlaced.DetachHex();
        }
        tilePlaced = null;

        GoToHand(1f);
        border.transform.rotation = Quaternion.identity;
    }

    public void SetInHand(Vector3 position, Vector3 rotation, int order) {
        positionInHand = position;
        rotationInHand = rotation;

        orderInHand = order;
    }

    public void GoToHand(float delay = 1f) {
        moveTween.ChangeValues(transform.position, positionInHand, delay).Restart();
        rotateTween.ChangeValues(transform.rotation.eulerAngles, rotationInHand, delay / 2).Restart();
        scaleTween.ChangeValues(transform.localScale, scaleInHand, delay / 2).Restart();

        ChangeSortingOrder(orderInHand);

        collider.enabled = false;
        StartCoroutine(Utils.WaitForSec(delay, () => {collider.enabled = true;}));
    }

    public void Vanish(Vector3 position, float delay = 1f) {
        moveTween.ChangeValues(transform.position, position, delay).Restart();
        
        collider.enabled = false;
        StartCoroutine(Utils.WaitForSec(delay, () => {collider.enabled = true;}));
    }

    public void ChangeSortingOrder(int order) {
        GetComponent<Renderer>().sortingOrder = order;
        nameTMP.sortingOrder = order + 1;
        spriteRenderer.sortingOrder = order + 2;
        border.GetComponent<Renderer>().sortingOrder = order + 3;
    }

    public void Do()
    {
        Debug.Log("과연 성공할 것인가?");
    }
}
