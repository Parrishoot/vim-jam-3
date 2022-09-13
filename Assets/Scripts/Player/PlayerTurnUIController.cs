using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnUIController: MonoBehaviour
{
    public TextMeshProUGUI cardsLeftText;
    public Button processTurnButton;


    public void SetText(string cardsLeft)
    {
        cardsLeftText.SetText(cardsLeft);
    }

    public void DisableButton()
    {
        processTurnButton.interactable = false;
    }

    public void EnableButton()
    {
        processTurnButton.interactable = true;
    }
}
