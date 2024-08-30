using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BattleTile : MonoBehaviour {
    public Vector3Int coordinate;
    public Card card;
    public Hex hex;

    [Header("필수 사전 설정")]
    [SerializeField]
    new SpriteRenderer renderer;
    [SerializeField]
    SpriteRenderer borderRenderer;
    Color originalColor;
    Color originalBorderColor;

    public bool isEmpty {
        get {
            if(hex != null && hex.state == Enums.HexState.Operating) {
                return false;
            }
            else {
                return true;
            }
        }
    }

    public void Init(Vector3Int coordinate) {
        BattleBoard board = BattleManager.instance.board;

        card = null;
        hex = null;

        this.coordinate = coordinate;
        transform.SetParent(board.transform);
        transform.position = Utils.ConvertToPosition(this.coordinate, board.tileSize) + board.transform.position;

        name = coordinate.ToString();

        originalBorderColor = borderRenderer.color;
        originalColor = renderer.color;

        borderRenderer.enabled = false;
        // powerCountTMP.enabled = false;
    }

    public void SetCard(Card card) {
        this.card = card;
    }

    public Hex AssignHex(Hex hex) {
        if(this.hex != hex) {
            renderer.sprite = hex.sprite;
            renderer.transform.DOScale(new Vector3(0, 0, 1), 0.2f)
            .From();
        }

        this.hex = hex;
        hex.SetTilePlaced(this);

        return hex;
    }

    public void DetachHex() {
        this.card = null;
        this.hex = null;

        renderer.sprite = null;
    }

    public void TurnOnHighlight(Color color) {
        renderer.color = color;
    }
    public void TurnOffHighlight() {
        renderer.color = originalColor;
    }

    public void TurnOnBorderHighlight(Color color) {
        borderRenderer.enabled = true;
        borderRenderer.color = color;
    }
    public void TurnOffBorderHighlight() {
        borderRenderer.enabled = false;
        borderRenderer.color = originalBorderColor;
    }
}