using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierPassive : PassiveController
{

    public override void Process()
    {
        foreach(Warrior warrior in GameObject.FindObjectsOfType<Warrior>())
        {
            warrior.AddDamageModifier(card.damageModifier);
        }

        base.Process();
    }
}
