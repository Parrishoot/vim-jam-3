using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveController : MonoBehaviour
{
    public enum PASSIVE_TARGET_TYPE
    {
        SELF,
        OPPONENT
    }

    public enum PASSIVE_APPLY_PHASE
    {
        BEFORE,
        DURING,
        AFTER
    }


    public PASSIVE_TARGET_TYPE passiveTargetType = PASSIVE_TARGET_TYPE.SELF;

    public PASSIVE_APPLY_PHASE passiveApplyPhase = PASSIVE_APPLY_PHASE.DURING;

    public virtual void Process()
    {
        turnCount -= 1;
    }

    private int turnCount = 1;

    protected TurnController turnController;
    protected Card card;

    public virtual void Despawn()
    {
        GameObject.Destroy(gameObject);
    }

    public bool Finished()
    {
        return turnCount <= 0;
    }

    public bool TargetsEnemy()
    {
        return passiveTargetType == PASSIVE_TARGET_TYPE.OPPONENT;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        turnCount = card.turnAmount == Card.INFINITE_TURNS ? int.MaxValue : card.turnAmount;
    }

    public void SetTurnController(TurnController turnController)
    {
        this.turnController = turnController;
    }

    public TurnController GetTurnController()
    {
        return turnController;
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
