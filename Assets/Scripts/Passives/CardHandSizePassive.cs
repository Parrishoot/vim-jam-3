using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandSizePassive : PassiveController
{
    private bool applied = false;

    public override string GetText()
    {
        return StringUtils.GetHandSizeAdjustAmountText(card.handSizeAdjustAmount) + StringUtils.GetCardPostfix(turnCount);
    }

    public override void Process()
    {
        if(!applied)
        {
            turnController.handSize += 1;
            applied = true;
        }
   
        base.Process();
    }

    public override void Despawn()
    {
        turnController.handSize -= 1;
        base.Despawn();
    }
}
