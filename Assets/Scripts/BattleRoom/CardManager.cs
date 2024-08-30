using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardManager : MonoBehaviour {
    [Header("필수 사전 설정")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] public Transform graveyardPoint;
    [SerializeField] GameObject cardPrefab;

    [SerializeField]
    List<Card> cardsInDeck;
    [SerializeField]
    List<Card> cardsInHand;
    [SerializeField]
    List<Card> cardsInBoard;
    [SerializeField]
    List<Card> cardsInGraveyard;
    [SerializeField]
    List<Card> cardsInVoid;

    public void Init(List<Hex> hexes) {
        cardsInDeck = new List<Card>();
        cardsInHand = new List<Card>();
        cardsInBoard = new List<Card>();
        cardsInGraveyard = new List<Card>();
        cardsInVoid = new List<Card>();

        foreach(Hex hex in hexes) {
            Card card = MakeCard(hex);
            cardsInDeck.Add(card);
        }

        // cardsInDeck.Shuffle();
    }

    public Card MakeCard(Hex hex) {
        Card card = Instantiate(cardPrefab, transform).GetComponent<Card>();
        card.SetPosition(spawnPoint.position);
        card.Init(hex);

        card.name = hex.name;

        return card;
    }

    public int GetHandCount() {
        return cardsInHand.Count;
    }

    public Card GetCardInHand(int idx) {
        if(idx >= cardsInHand.Count) {
            return null;
        }
        else {
            return cardsInHand[idx];
        }
    }
    
    public List<Card> GetCardsInHand() {
        return cardsInHand;
    }

    public List<Card> GetCardsInGraveyard() {
        return cardsInGraveyard;
    }

    public List<Card> GetCardsInDeck() {
        return cardsInDeck;
    }
    

    public void AddCardToDeck(Card card) {
        Card newCard = Instantiate(card, transform);

        newCard.transform.position = spawnPoint.position;
        cardsInDeck.Add(newCard);
        cardsInDeck.Shuffle();
    }

    public Card MoveDeckToHand() {
        if(cardsInDeck.Count == 0) {
            if(cardsInGraveyard.Count == 0) {
                return null;
            }
            
            cardsInDeck.AddRange(cardsInGraveyard);
            cardsInGraveyard.Clear();
            cardsInDeck.Shuffle();
        }

        Card drawnCard = cardsInDeck[cardsInDeck.Count - 1];
        drawnCard.SetPosition(spawnPoint.position);
        cardsInHand.Add(drawnCard);
        cardsInDeck.Remove(drawnCard);

        return drawnCard;
    }

    public void MoveHandToBoard(Card card) {
        if(card == null || cardsInHand.Contains(card) == false) {
            return;
        }

        card.SetPosition(graveyardPoint.position);
        cardsInHand.Remove(card);
        cardsInBoard.Add(card);
    }

    public void MoveHandToGraveyard(Card card) {
        if(card == null || cardsInHand.Contains(card) == false) {
            return;
        }

        // card.Vanish(graveyardPoint.position);
        cardsInHand.Remove(card);
        cardsInGraveyard.Add(card);
    }

    public void MoveAllHandToGraveyard() {
        cardsInGraveyard.AddRange(cardsInHand);
        cardsInHand.Clear();    
    }
    
    public void MoveHandToVoid(Card card) {
        if(card == null || cardsInHand.Contains(card) == false) {
            return;
        }

        // card.Vanish(graveyardPoint.position);
        cardsInHand.Remove(card);
        cardsInVoid.Add(card);
    }

    public void MoveBoardToGraveyard(Card card) {
        if(card == null || cardsInBoard.Contains(card) == false) {
            return;
        }

        cardsInBoard.Remove(card);
        cardsInGraveyard.Add(card);
    }

    public void MoveBoardToVoid(Card card) {
        if(card == null || cardsInBoard.Contains(card) == false) {
            return;
        }

        cardsInBoard.Remove(card);
        cardsInVoid.Add(card);
    }

    public void MoveGraveyardToVoid(Card card) {
        if(card == null || cardsInGraveyard.Contains(card) == false) {
            return;
        }

        cardsInGraveyard.Remove(card);
        cardsInVoid.Add(card);
    }

    public Card GetRandomCardInHand() {
        if(cardsInHand.Count == 0) {
            return null;
        }
        else {
            int rand = Random.Range(0, cardsInHand.Count);
            return cardsInHand[rand];
        }
    }
}