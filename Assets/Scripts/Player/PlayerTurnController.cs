using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnController : Singleton<PlayerTurnController>
{
    public List<GameObject> cards = new List<GameObject>();
    public int handSize = 4;
    public WarriorSpawner warriorSpawner;

    public Animator fireAnimator;

    private enum TURN_STATES
    {
        PENDING,
        DRAWING,

    }

    private bool drawing = false;

    private List<CardController> currentHand = new List<CardController>();
    private PlayerTurnUIController playerTurnUIController;

    public void Start()
    {
        playerTurnUIController = GetComponent<PlayerTurnUIController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !drawing)
        {
            StartCoroutine(DealCards());
        }

        int pendingCards = GetNumberOfPendingCards();

        if(pendingCards == 0)
        {
            playerTurnUIController.EnableButton();
        }
        else
        {
            playerTurnUIController.DisableButton();
        }

        playerTurnUIController.SetText(pendingCards.ToString());
    }


    public int GetNumberOfPendingCards()
    {
        return currentHand.FindAll(card => card.IsPending()).Count;
    }

    public void DealCard()
    {
        GameObject card = cards[Random.Range(0, cards.Count)];
        GameObject newCard = GameObject.Instantiate(card);

        currentHand.Add(newCard.GetComponent<CardController>());
        newCard.transform.SetParent(HandTransform.GetInstance().GetTransform(), false);
    }

    public void ClearHand()
    {
        foreach (CardController cardController in currentHand)
        {
            cardController.Despawn();
        }

        currentHand = new List<CardController>();
    }

    IEnumerator DealCards()
    {
        drawing = true;

        ClearHand();

        for (int i = 0; i < handSize; i++)
        {
            DealCard();
            yield return new WaitForSeconds(1f);
        }

        drawing = false;
    }

    IEnumerator ProcessSacrifices(List<CardController> sacrificeControllers)
    {
        foreach (CardController sacrificeController in sacrificeControllers)
        {
            sacrificeController.Despawn();

            yield return new WaitForSeconds(.25f);
        }

        yield return new WaitForSeconds(1f);

        fireAnimator.SetTrigger("burst");

        yield return new WaitForSeconds(2f);
    }

    IEnumerator ProcessWarriors(List<CardController> warriorControllers)
    {
        playerTurnUIController.DisableButton();

        List<Warrior> warriors = new List<Warrior>();

        foreach(CardController warriorController in warriorControllers)
        {
            Warrior warrior = warriorSpawner.SpawnWarrior(warriorController.card.warriorPrefab, warriorController.combatPower);

            warriorController.Despawn();

            warriors.Add(warrior);

            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(.25f);

        foreach(Warrior warrior in warriors) {

            warrior.Charge();

            yield return new WaitForSeconds(.25f);

        }
    }

    public IEnumerator Turn()
    {
        List<CardController> sacrificeControllers = currentHand.FindAll(card => card.GetDestiny() == CardController.CARD_DESTINY.SACRIFICE);
        List<CardController> warriorControllers = currentHand.FindAll(card => card.GetDestiny() == CardController.CARD_DESTINY.WARRIOR);

        // TODO: ADD THE PASSIVES HERE
        yield return StartCoroutine(ProcessSacrifices(sacrificeControllers));

        yield return StartCoroutine(ProcessWarriors(warriorControllers));

        currentHand = new List<CardController>();
    }

    public void ProcessTurn()
    {
        StartCoroutine(Turn());
    }
}
