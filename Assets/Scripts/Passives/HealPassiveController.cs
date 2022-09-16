using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPassiveController : PassiveController
{
    public override string GetText()
    {
        return StringUtils.GetShieldText(card.healAmount) + StringUtils.GetCardPostfix(turnCount);
    }

    public override void Process()
    {
        turnController.healthController.Heal(card.healAmount);

        base.Process();
    }
}
