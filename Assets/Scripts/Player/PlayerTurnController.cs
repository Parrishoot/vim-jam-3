using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnController : TurnController
{
    
    public NextTurnButtonController nextTurnButtonController;

    public override void Update()
    {
        base.Update();

        if(turnState == TURN_STATE.ACTIVE)
        {
            int pendingCards = GetNumberOfPendingCards();

            string buttonString;

            if (pendingCards == 0)
            {
                nextTurnButtonController.EnableButton();
                buttonString = "Continue";
            }
            else
            {
                nextTurnButtonController.DisableButton();
                buttonString = "Select " + pendingCards.ToString() + " more card" + (pendingCards > 1 ? "s" : "");
            }

            nextTurnButtonController.SetText(buttonString);
        }
    }

    public override IEnumerator DealCards()
    {
        // base.DealCards() wasnt working so looks like we're copying...
        yield return StartCoroutine(base.DealCards());

        nextTurnButtonController.ActivateButton();
    }

    public int GetNumberOfPendingCards()
    {
        return currentHand.FindAll(card => card.IsPending()).Count;
    }

    public override void ProcessTurn()
    {
        nextTurnButtonController.DeactivateButton();

        base.ProcessTurn();
    }

    public override TurnController GetOpponentTurnController()
    {
        return FindObjectOfType<EnemyTurnController>();
    }
}
