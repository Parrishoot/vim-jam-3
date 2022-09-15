using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveController : MonoBehaviour
{

    public abstract void ProcessBeforeTurn();

    public abstract void ProcessDuringTurn();

    public abstract void ProcessAfterTurn();

    private int turnCount = 1;
    protected Card card;

    public void FinishTurn()
    {
        turnCount -= 1;
    }

    public void Despawn()
    {
        Debug.Log("Despawning!");
        GameObject.Destroy(gameObject);
    }

    public bool Finished()
    {
        return turnCount <= 0;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        turnCount = card.turnAmount == Card.INFINITE_TURNS ? int.MaxValue : card.turnAmount;
    }

    public void ShowPassive()
    {
        // TODO: Implement this
    }

    public void HidePassive()
    {
        // TODO: Implement this
    }
}
