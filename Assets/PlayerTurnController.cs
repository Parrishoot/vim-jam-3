using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnController : Singleton<PlayerTurnController>
{
    public List<GameObject> cards = new List<GameObject>();
    public int handSize = 4;

    private List<CardController> currentHand = new List<CardController>();

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DealCards());
        }
    }

    public void DealCard()
    {
        GameObject card = cards[Random.Range(0, cards.Count)];
        GameObject newCard = GameObject.Instantiate(card);

        currentHand.Add(newCard.GetComponent<CardController>());
        newCard.transform.SetParent(HandTransform.GetInstance().GetTransform(), false);
    }

    public void ClearHand()
    {
        foreach(CardController cardController in currentHand) {
            cardController.Despawn();
        }

        currentHand = new List<CardController>();
    }

    IEnumerator DealCards()
    {
        ClearHand();

        for(int i = 0; i < handSize; i++)
        {
            DealCard();
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
