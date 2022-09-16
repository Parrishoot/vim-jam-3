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

        textAttributes.Add(GetDamageModifierText());
        textAttributes.Add(GetShieldText());
        textAttributes.Add(GetHealText());
        textAttributes.Add(GetLifeStealText());
        textAttributes.Add(GetHandSizeAdjustAmountText());

        textAttributes.RemoveAll(s => s == "");

        return (string.Join(" and ", textAttributes) + GetCardPostfix()).ToUpper();
    }

    private string GetDamageModifierText()
    {
        return damageModifier == 1 ? "" : "Deal " + damageModifier.ToString() + "x damage";
    }

    private string GetShieldText()
    {
        return shieldAmount <= 0 ? "" : "Shield up to " + shieldAmount.ToString() + " damage";
    }

    private string GetHealText()
    {
        return healAmount <= 0 ? "" : "Heal " + healAmount.ToString() + " HP at the end of your turn";
    }

    private string GetLifeStealText()
    {
        return stealAmount <= 0 ? "" : "Steal " + stealAmount.ToString() + " HP from your opponent at the end of your turn";
    }

    private string GetHandSizeAdjustAmountText()
    {
        return handSizeAdjustAmount == 0 ? "" : "Adjust hand size by " + handSizeAdjustAmount.ToString() + " card" + (Mathf.Abs(handSizeAdjustAmount) > 0 ? "s" : "");
    }

    private string GetCardPostfix()
    {
        string postfix = " ";

        switch(turnAmount)
        {
            case 1:
                return postfix + "this turn";

            case -1:
                return postfix + "indefinitely";

            default:
                return postfix + "for " + turnAmount.ToString() + " turns";
        }
    }
}
