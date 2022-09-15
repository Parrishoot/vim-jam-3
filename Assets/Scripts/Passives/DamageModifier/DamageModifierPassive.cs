using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierPassive : PassiveController
{
    public override void ProcessAfterTurn()
    {
        
    }

    public override void ProcessBeforeTurn()
    {
        
    }

    public override void ProcessDuringTurn()
    {
        foreach(Warrior warrior in GameObject.FindObjectsOfType<Warrior>())
        {
            warrior.AddDamageModifier(card.damageModifier);
        }
    }
}
