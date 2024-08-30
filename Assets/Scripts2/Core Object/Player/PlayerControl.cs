using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour {
    public bool canAct;
    public bool canOver;

    enum State {
        Waiting,
        Dragging,
        PlacedTemp
    }
    State state;
    
    Card selectedCard;
    BattleTile selectedTile;
    Card focusedCard;

    [Header("필수 사전 설정")]
    [SerializeField]
    GameObject dimObj;
    [SerializeField]
    GameObject detailsObj;

    Enums.Direction preDir;

    void Update() {
        if(selectedCard == null) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
            
            // Mouse Over
            if(hit && hit.collider.CompareTag("Card")) {
                Card card = hit.collider.GetComponent<Card>();
                if(canOver) {
                   ShowCardDetails(card);
                }

                // Drag 시작
                if(Input.GetMouseButtonDown(0) && canAct) {
                    state = State.Dragging;

                    preDir = Enums.Direction.None; 
                    focusedCard = null;
                    selectedCard = card;
                    selectedCard.StartDragForMove(new Vector3(0.4f, 0.4f, 1));
                }
            }
            else {
                // Mouse Exit
                HideCardDetails();
            }

            if(hit && hit.collider.CompareTag("Monster")) {
                Monster monster = hit.collider.GetComponent<Monster>();

                if(Input.GetMouseButtonDown(0) && canAct) {
                    BattleManager.instance.SetTarget(monster);
                }
            }
        }
        else if(state == State.Dragging) {
            LayerMask boardMask = LayerMask.GetMask("Board");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f, boardMask);

            if(Input.GetMouseButton(0)) {
                if(hit) {
                    BattleTile tile = hit.collider.GetComponent<BattleTile>();

                    if(selectedTile == null || selectedTile != tile) {
                        selectedTile = tile;
                        BattleManager.instance.board.indicator.TurnOn(tile, selectedCard.hex.scope);
                    }
                    selectedCard.SetPosition(tile.transform.position);
                }
                else {
                    BattleManager.instance.board.indicator.TurnOff();
                    selectedTile = null;
                    
                    selectedCard.SetPosition(mousePos);
                }
            }
            else if(Input.GetMouseButtonUp(0)) {
                if(hit) {
                    state = State.PlacedTemp;

                    selectedTile = hit.collider.GetComponent<BattleTile>();
                    selectedCard.PlaceTemp(selectedTile);

                    SetDimActive(true);
                }
                else {
                    CancelSelect();
                }
            }
        }
        else if(state == State.PlacedTemp) {
            // 배치 중일 때 우클릭하면 취소
            if(Input.GetMouseButtonDown(1)) {
                SetDimActive(false);
                CancelSelect();
                return;
            }

            // 카드 안에선 가만히 있음
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
            if(hit && hit.collider.gameObject == selectedTile.gameObject) {
                return;
            }

            // 배치 중일 때 드래그
            if(Input.GetMouseButton(0)) {
                Enums.Direction dir = CalculateMouseDirection();
                if(preDir != dir) {
                    selectedCard.ChangeTiles(dir);
                    BattleManager.instance.board.indicator.TurnOn(selectedTile, selectedCard.hex.scope);

                    preDir = dir;
                }
            }
            // 배치 중일 때 드래그 종료
            else if(Input.GetMouseButtonUp(0)) {
                state = State.Waiting;
                
                SetDimActive(false);
                BattleManager.instance.board.indicator.TurnOff();
                selectedCard.Place();

                selectedCard = null;
                selectedTile = null;
            }
        }
    }

    void CancelSelect() {
        state = State.Waiting;

        BattleManager.instance.board.indicator.TurnOff();

        selectedCard.Cancel();
        selectedCard = null;
        selectedTile = null;
        focusedCard = null;
    }

    void ShowCardDetails(Card card) {
        if(detailsObj.activeSelf == false) {
            detailsObj.SetActive(true);
            
            focusedCard = card;
            focusedCard.Focus();
            // detailsObj.GetComponent<CardDetails>().Set(card.hex);
        }
        else if(focusedCard != null && card != focusedCard) {
            focusedCard.GoToHand(0.1f);

            focusedCard = card;
            focusedCard.Focus();
            // detailsObj.GetComponent<CardDetails>().Set(card.hex);
        }
    }
    void HideCardDetails() {
        if(detailsObj.activeSelf == true) {
            detailsObj.SetActive(false);
            detailsObj.GetComponent<HexDetailsUI>().Clear();

            if(focusedCard) {
                focusedCard.GoToHand(0.1f);
                focusedCard = null;
            }
        }
    }

    void SetDimActive(bool isActive) {
        if(dimObj.activeSelf != isActive) {
            dimObj.SetActive(isActive);
        }
    }

    Enums.Direction CalculateMouseDirection() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tilePos = selectedTile.transform.position;

        float angle = Quaternion.FromToRotation(mousePos - tilePos, new Vector2(0f, 1f)).eulerAngles.z;
        
        return (Enums.Direction) (((int) Enums.Direction.TopRight + ((int) angle / 60)) % 6);
    }
}
