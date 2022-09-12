using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Basic Card", order = 200)]
public class Card : ScriptableObject
{
    private const int INFINITE = -1;

    public enum PASSIVE_TYPE
    {
        HAND_SIZE,
        ADD_EFFECT,
        BOLSTER,
        HEAL
    }

    public enum PASSIVE_APPLY
    {
        BEFORE,
        DURING,
        AFTER
    }

    public string title = "TITLE";

    public Vector2 combatPowerBounds;

    public int turnAmount = INFINITE;

    public PASSIVE_APPLY passiveApply = PASSIVE_APPLY.DURING;

    public override string ToString()
    {
        return "Do something " + GetCardPostfix();
    }

    public string GetCardPostfix()
    {
        string postfix = "";

        switch(passiveApply)
        {
            case PASSIVE_APPLY.BEFORE:
                postfix += "before your turn ";
                break;
            case PASSIVE_APPLY.AFTER:
                postfix += "after your turn ";
                break;
        }

        string turnString = turnAmount != INFINITE ? "for " + turnAmount.ToString() + " turns" : "";

        return postfix + turnString;
    }
}
