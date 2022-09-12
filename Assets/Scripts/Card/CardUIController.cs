using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIController : MonoBehaviour
{

    public Color warriorColor;
    public Color sacrificeColor;

    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI sacrificeTMP;
    public TextMeshProUGUI combatPowerTMP;

    public Image backgroundImage;

    public void SetText(string cardTitle, string sacrificeText, string combatPowerText)
    {
        titleTMP.text = cardTitle;
        sacrificeTMP.text = sacrificeText;
        combatPowerTMP.text = combatPowerText;
    }

    public void SetDestinyColor(CardController.CARD_DESTINY destiny)
    {
        backgroundImage.color = destiny == CardController.CARD_DESTINY.WARRIOR ? warriorColor : sacrificeColor;
    }
}
