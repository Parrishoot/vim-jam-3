using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnController : TurnController
{
    
    private PlayerTurnUIController playerTurnUIController;

    public void Start()
    {
        playerTurnUIController = GetComponent<PlayerTurnUIController>();
    }

    public override void Update()
    {
        base.Update();

        int pendingCards = GetNumberOfPendingCards();

        if(pendingCards == 0)
        {
            playerTurnUIController.EnableButton();
        }
        else
        {
            playerTurnUIController.DisableButton();
        }

        playerTurnUIController.SetText(pendingCards.ToString());
    }

    public override IEnumerator DealCards()
    {
        // base.DealCards() wasnt working so looks like we're copying...
        yield return StartCoroutine(base.DealCards());

        playerTurnUIController.ActivateButton();
    }

    public int GetNumberOfPendingCards()
    {
        return currentHand.FindAll(card => card.IsPending()).Count;
    }

    public override void ProcessTurn()
    {
        playerTurnUIController.DeactivateButton();

        base.ProcessTurn();
    }
}
