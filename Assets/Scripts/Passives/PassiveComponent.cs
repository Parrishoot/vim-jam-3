using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveComponent : MonoBehaviour
{
    private Card card;
    private PassiveController parentPassiveController;

    public Card GetCard()
    {
        return card;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }

    public PassiveController GetParent()
    {
        return parentPassiveController;
    }

    public void SetPassiveController(PassiveController parentPassiveController)
    {
        this.parentPassiveController = parentPassiveController;
    }
}
