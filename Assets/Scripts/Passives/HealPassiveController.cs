using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPassiveController : PassiveController
{
    public override void Process()
    {
        turnController.healthController.Heal(card.healAmount);

        base.Process();
    }
}
