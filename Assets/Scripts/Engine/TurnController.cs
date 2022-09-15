using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public int handSize = 1;
    public WarriorSpawner warriorSpawner;

    public Animator fireAnimator;

    public Transform cardSpawnTransform;
    public Transform passiveTransform;

    public Warrior.TARGET_TYPE warriorTargetType;

    private List<PassiveController> beforePassiveControllers = new List<PassiveController>();
    private List<PassiveController> afterPassiveControllers = new List<PassiveController>();
    private List<PassiveController> duringPassiveControllers = new List<PassiveController>();

    protected enum TURN_STATE
    {
        ACTIVE,
        FINISHED
    }

    protected TURN_STATE turnState = TURN_STATE.FINISHED;

    protected bool drawing = false;

    protected List<CardController> currentHand = new List<CardController>();

    public virtual void ProcessTurn() {

        StartCoroutine(Turn());
    
    }

    public void EndTurn()
    {
        turnState = TURN_STATE.FINISHED;
        GameController.GetInstance().SwitchTurns();
    }

    public virtual void StartTurn()
    {
        StartCoroutine(DealCards());
        turnState = TURN_STATE.ACTIVE;
    }

    public virtual void Update()
    {

    }

    public void DealCard()
    {
        GameObject newCard = CardListController.GetInstance().GetRandomCardObject();

        currentHand.Add(newCard.GetComponent<CardController>());
        newCard.transform.SetParent(cardSpawnTransform, false);
    }

    public void ClearHand()
    {
        foreach (CardController cardController in currentHand)
        {
            cardController.Despawn();
        }

        currentHand = new List<CardController>();
    }

    public virtual IEnumerator DealCards()
    {
        ClearHand();

        for (int i = 0; i < handSize; i++)
        {
            DealCard();
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator ProcessSacrifices(List<CardController> sacrificeControllers)
    {
        foreach (CardController sacrificeController in sacrificeControllers)
        {
            foreach(GameObject passiveControllerPrefab in sacrificeController.card.passiveControllerPrefabs)
            {
                GameObject passiveControllerObj = GameObject.Instantiate(passiveControllerPrefab);
                passiveControllerObj.transform.SetParent(passiveTransform);

                PassiveController passiveController = passiveControllerObj.GetComponent<PassiveController>();
                passiveController.SetCard(sacrificeController.card);

                switch(sacrificeController.card.passiveApply)
                {
                    case Card.PASSIVE_APPLY.BEFORE:
                        beforePassiveControllers.Add(passiveController);
                        break;

                    case Card.PASSIVE_APPLY.DURING:
                        duringPassiveControllers.Add(passiveController);
                        break;

                    case Card.PASSIVE_APPLY.AFTER:
                        afterPassiveControllers.Add(passiveController);
                        break;
                }
            }

            sacrificeController.Despawn();

            yield return new WaitForSeconds(.25f);
        }

        yield return new WaitForSeconds(1f);

        fireAnimator.SetTrigger("burst");
        CameraManager.GetInstance().ScreenShake(.05f, 1f, 500f);

        yield return new WaitForSeconds(2f);
    }

    public IEnumerator ProcessWarriors(List<CardController> warriorControllers)
    {

        List<Warrior> warriors = new List<Warrior>();

        foreach (CardController warriorController in warriorControllers)
        {
            Warrior warrior = warriorSpawner.SpawnWarrior(warriorController.card.warriorPrefab, warriorController.combatPower, warriorTargetType);

            warriorController.Despawn();

            warriors.Add(warrior);

            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(.5f);

        foreach(PassiveController passiveController in duringPassiveControllers)
        {
            passiveController.ShowPassive();
            passiveController.ProcessDuringTurn();

            yield return new WaitForSeconds(1f);

            passiveController.HidePassive();
        }

        foreach (Warrior warrior in warriors)
        {

            warrior.Charge();

            yield return new WaitForSeconds(.5f);

        }
    }

    public void CheckForControllerExpiration(List<PassiveController> passiveControllers)
    {
        List<PassiveController> controllersToDelete = new List<PassiveController>();

        for(int i = 0; i < passiveControllers.Count; i++)
        {
            passiveControllers[i].FinishTurn();

            if(passiveControllers[i].Finished())
            {
                controllersToDelete.Add(passiveControllers[i]);
            }
        }

        foreach (PassiveController controllerToDelete in controllersToDelete)
        {
            passiveControllers.Remove(controllerToDelete);
            controllerToDelete.Despawn();
        }

    }

    public IEnumerator Turn()
    {
        List<CardController> sacrificeControllers = currentHand.FindAll(card => card.GetDestiny() == CardController.CARD_DESTINY.SACRIFICE);
        List<CardController> warriorControllers = currentHand.FindAll(card => card.GetDestiny() == CardController.CARD_DESTINY.WARRIOR);

        // TODO: ADD THE PASSIVES HERE
        yield return StartCoroutine(ProcessSacrifices(sacrificeControllers));

        yield return StartCoroutine(ProcessWarriors(warriorControllers));

        foreach(PassiveController passiveController in afterPassiveControllers)
        {
            passiveController.ProcessAfterTurn();
        }

        CheckForControllerExpiration(beforePassiveControllers);
        CheckForControllerExpiration(duringPassiveControllers);
        CheckForControllerExpiration(afterPassiveControllers);

        currentHand = new List<CardController>();

        EndTurn();
    }

    public bool IsFinished()
    {
        return false;
    }

}
