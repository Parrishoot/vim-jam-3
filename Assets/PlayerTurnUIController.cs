using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnUIController: MonoBehaviour
{
    public TextMeshProUGUI cardsLeftText;

    public void SetText(string cardsLeft)
    {
        cardsLeftText.SetText(cardsLeft);
    }
}
