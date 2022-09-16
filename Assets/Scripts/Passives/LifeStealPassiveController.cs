using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealPassiveController : PassiveController
{
    public override void Process()
    {
        int damageDealt = turnController.GetOpponentTurnController().healthController.Damage(card.stealAmount);

        turnController.healthController.Heal(damageDealt);

        base.Process();
    }
}
