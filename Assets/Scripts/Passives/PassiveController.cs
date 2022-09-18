using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveController : MonoBehaviour
{
    public PassiveUIController rightPassiveUIController;
    public PassiveUIController leftPassiveUIController;

    protected PassiveUIController passiveUIController;

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

        if(Finished())
        {
            passiveUIController.Despawn();
        }
    }

    public abstract string GetText();

    public int turnCount = 1;

    protected TurnController turnController;
    protected Card card;

    public virtual void Despawn()
    {
        GameObject.Destroy(gameObject);
    }

    public virtual bool Finished()
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

        if(turnController.passiveTransform.GetComponentInParent<PassiveShower>().leftSide)
        {
            passiveUIController = leftPassiveUIController;
            Destroy(rightPassiveUIController.gameObject);
        }
        else
        {
            passiveUIController = rightPassiveUIController;
            Destroy(leftPassiveUIController.gameObject);
        }
    }

    public void Update()
    {
        if(passiveUIController != null)
        {
            passiveUIController.SetText(GetText().ToUpper());
        }
    }

    public TurnController GetTurnController()
    {
        return turnController;
    }

    public void ShowPassive()
    {
        passiveUIController.SetBorderActive();
    }

    public void HidePassive()
    {
        if(passiveUIController != null)
        {
            passiveUIController.SetBorderInactive();
        }
    }
}
