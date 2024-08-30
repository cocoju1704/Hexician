using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardChoice : MonoBehaviour {
    [SerializeField] GameObject contentObj;
    [SerializeField] GameObject slotObj;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPoint;
    [SerializeField] Type type;

    public enum Type {
        Player,
        Deck,
        Graveyard
    }

    List<Hex> hexes;

    List<Card> cards;
    List<WorldObjectImage> images;
    List<ObjectHolder> holders;

    void Awake() {
        cards = new List<Card>();
        images = new List<WorldObjectImage>();
        holders = new List<ObjectHolder>();
    }

    public void Open() {
        SetHexes(new List<int> { 1, 1, 1, 1, 1 });

        while(images.Count < hexes.Count) {
            MakeNewSlot();
        }

        int count = images.Count;
        for(int i = 0; i < count; i++) {
            if(i < hexes.Count) {
                cards[i].gameObject.SetActive(true);
                images[i].gameObject.SetActive(true);

                cards[i].SetHex(hexes[i]);

                StartCoroutine(images[i].Render(cards[i].gameObject));
                holders[i].SetObject(cards[i].gameObject);
            }
            else {
                cards[i].gameObject.SetActive(false);
                images[i].gameObject.SetActive(false);
            }
        }
    }

    void MakeNewSlot() {
        GameObject newSlot = Instantiate(slotObj);

        WorldObjectImage image = newSlot.GetComponent<WorldObjectImage>();
        image.transform.SetParent(contentObj.transform);
        image.transform.localScale = Vector3.one;
        images.Add(image);

        ObjectHolder holder = newSlot.GetComponent<ObjectHolder>();
        holders.Add(holder);

        Card card = Instantiate(cardPrefab).GetComponent<Card>();
        card.transform.position = cardPoint.position + new Vector3(270, 0, 0) * cards.Count;
        card.transform.localScale = Vector3.one;
        card.transform.SetParent(cardPoint.transform);
        cards.Add(card);
    }
    public void SetHexes(List<int> hexes) {
        this.hexes = ResourceSystem.instance.MakeHex(hexes);
    }
}
