using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIController : MonoBehaviour
{

    public Color warriorColor;
    public Color sacrificeColor;

    public float outlineThickness = .115f;

    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI sacrificeTMP;
    public TextMeshProUGUI combatPowerTMP;

    public Image backgroundImage;

    public void Start()
    {
        Material mat = Instantiate(backgroundImage.material);
        backgroundImage.material = mat;
    }

    public void SetText(string cardTitle, string sacrificeText, string combatPowerText)
    {
        titleTMP.text = cardTitle;
        sacrificeTMP.text = sacrificeText;
        combatPowerTMP.text = combatPowerText;
    }

    public void SetDestinyColor(CardController.CARD_DESTINY destiny)
    {
        Color color = destiny == CardController.CARD_DESTINY.WARRIOR ? warriorColor : sacrificeColor;
        backgroundImage.material.SetFloat("_OutlineThickness", outlineThickness);
        backgroundImage.material.SetColor("_OutlineColor", color);
    }
}
