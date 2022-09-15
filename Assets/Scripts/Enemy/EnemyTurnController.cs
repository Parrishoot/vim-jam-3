using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnController : TurnController
{

    public override IEnumerator DealCards()
    {
        yield return base.DealCards();

        foreach (CardController card in currentHand)
        {
            card.SetDestiny(Random.value < .5 ? CardController.CARD_DESTINY.WARRIOR : CardController.CARD_DESTINY.SACRIFICE);

            yield return new WaitForSeconds(.5f);
        }

        ProcessTurn();
    }
}
