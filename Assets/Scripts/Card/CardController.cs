using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public enum CARD_DESTINY {
        PENDING,
        WARRIOR,
        SACRIFICE
    }

    public AudioClip warriorDestinyClip;
    public AudioClip sacrificeDestinyClip;

    public Card card;

    public AudioSource audioSource;

    public PlayerDraggableCard playerDraggableCard;

    private CARD_DESTINY destiny;

    private CardUIController cardUIController;

    public bool interactable;

    public bool mainMenu = false;

    public int combatPower;

    public void Start()
    {
        cardUIController = GetComponent<CardUIController>();

        if (!mainMenu)
        {
            

            combatPower = (int)Random.Range(card.combatPowerBounds.x, card.combatPowerBounds.y);

            cardUIController.SetText(card.title, card.ToString(), combatPower.ToString());

            audioSource.Play();
        }
    }

    public void SetDestiny(CARD_DESTINY newDestiny)
    {
        destiny = newDestiny;

        audioSource.clip = newDestiny == CARD_DESTINY.SACRIFICE ? sacrificeDestinyClip : warriorDestinyClip;
        AudioUtils.PlaySoundWithRandomPitch(audioSource, .05f, 1f);

        cardUIController.SetDestinyColor(newDestiny);
    }

    public CARD_DESTINY GetDestiny()
    {
        return destiny;
    }

    public bool IsPending()
    {
        return GetDestiny() == CARD_DESTINY.PENDING;
    }

    public void Despawn()
    {
        Destroy(playerDraggableCard.parentObject.gameObject);
        Destroy(playerDraggableCard.gameObject);
        Destroy(gameObject);
    }
}
