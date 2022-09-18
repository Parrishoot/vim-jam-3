using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnButtonController : MonoBehaviour
{
    public TextMeshProUGUI cardsLeftText;
    public Button processTurnButton;

    public Animator animator;

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

    public void DeactivateButton()
    {
        animator.SetTrigger("flyOut");
        DisableButton();
    }

    public void ActivateButton()
    {
        animator.SetTrigger("flyIn");
    }
}
