using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListController : Singleton<CardListController>
{
    public List<Card> cards = new List<Card>();
    public GameObject cardPrefab;

    public GameObject GetRandomCardObject()
    {
        Card card = cards[Random.Range(0, cards.Count)];
        GameObject newCard = GameObject.Instantiate(cardPrefab);
        newCard.GetComponent<CardController>().card = card;

        return newCard;
    }
}
