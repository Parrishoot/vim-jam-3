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
        return stealAmount <= 0 ? "" : "Lifesteal " + stealAmount.ToString() + " HP at the end of your turn";
    }

    public static string GetHandSizeAdjustAmountText(int handSizeAdjustAmount)
    {
        return handSizeAdjustAmount == 0 ? "" : "Add " + handSizeAdjustAmount.ToString() + " card" + (Mathf.Abs(handSizeAdjustAmount) > 1 ? "s" : "");
    }

    public static string GetCardPostfix(int turnAmount)
    {
        string postfix = " ";

        switch (turnAmount)
        {
            case -1:
                return postfix + "indefinitely";

            default:
                return postfix + "for " + turnAmount.ToString() + " turn" + (turnAmount == 1 ? "" : "s");
        }
    }
}
