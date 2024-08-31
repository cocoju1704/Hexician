using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MyAnim : MonoBehaviour {
    static MyAnim _instance;
    public static MyAnim instance {
        get {
            if(_instance) {
                _instance = GameObject.Find("MyAnim").GetComponent<MyAnim>();
            }
            return _instance;
        }
    }

    void Awake() {
        if(_instance == null) {
            _instance = this;
        }
        else if(_instance != null) {
            Destroy(gameObject);
        }
    }

    public Tween GetBattleStartAnim() {
        GameObject msgObj = GameObject.Find("Message");

        Sequence seq = DOTween.Sequence()            
            .AppendCallback(() => {
                msgObj.GetComponent<TextMeshProUGUI>().text = "전투 시작";
            })
            .Append(msgObj.transform.DOScale(new Vector3(1, 1, 1), 0.2f))
            .Append(msgObj.transform.DOScale(Vector3.zero, 0.2f))
            .Pause();

        return seq;
    }

    public Tween GetTurnStartAnim() {
        GameObject msgObj = GameObject.Find("Message");

        Sequence seq = DOTween.Sequence()            
            .AppendCallback(() => {
                msgObj.GetComponent<TextMeshProUGUI>().text = "플레이어 턴 시작";
            })
            .Append(msgObj.transform.DOScale(new Vector3(1, 1, 1), 0.2f))
            .Append(msgObj.transform.DOScale(Vector3.zero, 0.2f))
            .Pause();

        return seq;
    }

    public Tween GetAlignHandAnim(float delay = 1f) {
        List<Card> cardsInHand = BattleManager.instance.cardManager.GetCardsInHand();

        int count = cardsInHand.Count;

        Vector2 startPos = new Vector2(0, -10f);

        float xOffset = 1f;
        float yOffset = 0.3f;
        float rot = 5;

        Sequence seq = DOTween.Sequence().Pause();

        if(count == 1) {
            Card card = cardsInHand[0];

            return seq
                .JoinCallback(() => {
                    card.SetInHand(startPos, Vector3.zero, 0);
                    card.GoToHand(delay);
                })
                .AppendInterval(delay);
        }
        else {
            for(int i = 0; i < count; i++) {
                float val = i / ((float) count - 1.0f);

                float xPos = Mathf.Lerp(count * -xOffset, count * xOffset, val);
                float yPos = -Mathf.Abs(Mathf.Lerp(count * -yOffset, count * yOffset, val)) + startPos.y;
                float zRot = Mathf.Lerp(count * rot, count * -rot, val);

                if(val == 0.5f) {
                    yPos -= 0.1f;
                }

                Card card = cardsInHand[i];
                int idx = i;
                seq.JoinCallback(() => {
                    card.SetInHand(new Vector3(xPos, yPos, 0), new Vector3(0, 0, zRot), idx * 10) ;
                    card.GoToHand(delay);
                });
            }   

            return seq.AppendInterval(delay);
        }
    }

    public Tween GetClearHandAnim() {
        List<Card> cardsInHand = BattleManager.instance.cardManager.GetCardsInHand();
        Transform graveyardPoint = BattleManager.instance.cardManager.graveyardPoint;

        Sequence seq = DOTween.Sequence().Pause();
        foreach(Card card in cardsInHand) {
            seq
            .AppendCallback(() => {
                card.Vanish(graveyardPoint.position, 0.2f);
            })
            .AppendInterval(0.2f);
        }

        return seq;
    }

    public Tween GetVanishCardAnim(Card card) {
        if(card == null) {
            return DOTween.Sequence().Pause();
        }

        Transform graveyardPoint = BattleManager.instance.cardManager.graveyardPoint;

        return DOTween.Sequence()
            .AppendCallback(() => {
                card.Vanish(graveyardPoint.position, 0.2f);
            })
            .AppendInterval(0.2f)
            .Pause();
    }

}