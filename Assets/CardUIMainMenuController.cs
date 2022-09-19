using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIMainMenuController : MonoBehaviour, IPointerDownHandler
{

    public CardController cardController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            cardController.SetDestiny(CardController.CARD_DESTINY.SACRIFICE);
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            cardController.SetDestiny(CardController.CARD_DESTINY.WARRIOR);
        }
    }
}
