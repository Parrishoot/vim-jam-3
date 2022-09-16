using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtils
{

    public static string GetDamageModifierText(float damageModifier)
    {
        return damageModifier == 1 ? "" : "Deal " + damageModifier.ToString() + "x damage";
    }

    public static string GetShieldText(float shieldAmount)
    {
        return shieldAmount <= 0 ? "" : "Shield up to " + shieldAmount.ToString() + " damage";
    }

    public static string GetHealText(int healAmount)
    {
        return healAmount <= 0 ? "" : "Heal " + healAmount.ToString() + " HP at the end of your turn";
    }

    public static string GetLifeStealText(int stealAmount)
    {
        return stealAmount <= 0 ? "" : "Steal " + stealAmount.ToString() + " HP from your opponent at the end of your turn";
    }

    public static string GetHandSizeAdjustAmountText(int handSizeAdjustAmount)
    {
        return handSizeAdjustAmount == 0 ? "" : "Adjust hand size by " + handSizeAdjustAmount.ToString() + " card" + (Mathf.Abs(handSizeAdjustAmount) > 0 ? "s" : "");
    }

    public static string GetCardPostfix(int turnAmount)
    {
        string postfix = " ";

        switch (turnAmount)
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
