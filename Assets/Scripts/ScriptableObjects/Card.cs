using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Basic Card", order = 200)]
public class Card : ScriptableObject
{
    public static int INFINITE_TURNS = -1;

    public string title = "TITLE";

    public Vector2 combatPowerBounds;

    public int turnAmount = INFINITE_TURNS;

    // THESE ARE THE DIFFERENT PASSIVE MODIFIERS
    // THEIR CONTROLLERS WILL AUTOMATICALLY BE FETCHED ONCE THESE
    // VALUES ARE CHANGED FROM THEIR DEFAULTS
    public float damageModifier = 1;
    public float shieldAmount = 0f;
    public int healAmount = 0;
    public int stealAmount = 0;
    public int handSizeAdjustAmount = 0;


    public override string ToString()
    {

        List<string> textAttributes = new List<string>();

        textAttributes.Add(StringUtils.GetDamageModifierText(damageModifier));
        textAttributes.Add(StringUtils.GetShieldText(shieldAmount));
        textAttributes.Add(StringUtils.GetHealText(healAmount));
        textAttributes.Add(StringUtils.GetLifeStealText(stealAmount));
        textAttributes.Add(StringUtils.GetHandSizeAdjustAmountText(handSizeAdjustAmount));

        textAttributes.RemoveAll(s => s == "");

        return (string.Join(" and ", textAttributes) + StringUtils.GetCardPostfix(turnAmount)).ToUpper();
    }

}
