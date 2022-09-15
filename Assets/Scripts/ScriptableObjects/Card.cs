using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Basic Card", order = 200)]
public class Card : ScriptableObject
{
    public static int INFINITE_TURNS = -1;

    public GameObject warriorPrefab;

    public List<GameObject> passiveControllerPrefabs;

    public enum TARGET_TYPE
    {
        SELF,
        OTHER
    }

    public enum PASSIVE_APPLY
    {
        BEFORE,
        DURING,
        AFTER
    }

    public string title = "TITLE";

    public Vector2 combatPowerBounds;

    public int turnAmount = INFINITE_TURNS;

    public PASSIVE_APPLY passiveApply = PASSIVE_APPLY.DURING;

    public TARGET_TYPE targetType = TARGET_TYPE.SELF;

    public float damageModifier = 1;

    public override string ToString()
    {

        List<string> textAttributes = new List<string>();

        textAttributes.Add(GetDamageModifierText());

        textAttributes.RemoveAll(s => s == "");

        return string.Join(" and ", textAttributes) + GetCardPostfix();
    }

    private string GetDamageModifierText()
    {
        return damageModifier == 1 ? "" : "Deal " + damageModifier.ToString() + "x damage";
    }

    private string GetCardPostfix()
    {
        string postfix = " ";

        switch(passiveApply)
        {
            case PASSIVE_APPLY.BEFORE:
                postfix += "before your turn ";
                break;
            case PASSIVE_APPLY.AFTER:
                postfix += "after your turn ";
                break;
        }

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
