using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public enum CARD_DESTINY {
        PENDING,
        WARRIOR,
        SACRIFICE
    }

    public Card card;

    public PlayerDraggableCard playerDraggableCard;

    private CARD_DESTINY destiny;

    private CardUIController cardUIController;

    private int combatPower;

    public void Start()
    {
        cardUIController = GetComponent<CardUIController>();

        combatPower = (int) Random.Range(card.combatPowerBounds.x, card.combatPowerBounds.y);

        cardUIController.SetText(card.title, card.ToString(), combatPower.ToString());
    }

    public void Update()
    {
        
    }

    public void SetDestiny(CARD_DESTINY newDestiny)
    {
        destiny = newDestiny;

        cardUIController.SetDestinyColor(newDestiny);
    }

    public CARD_DESTINY GetDestiny()
    {
        return destiny;
    }

    public bool IsPending()
    {
        return GetDestiny() == CARD_DESTINY.PENDING;
    }

    public void Despawn()
    {
        Destroy(playerDraggableCard.parentObject.gameObject);
        Destroy(playerDraggableCard.gameObject);
        Destroy(gameObject);
    }
}
