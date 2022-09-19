using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnController : MonoBehaviour
{
    public int handSize = 1;
    public WarriorSpawner warriorSpawner;

    public Animator fireAnimator;

    public Transform cardSpawnTransform;
    public Transform passiveTransform;

    public Warrior.TARGET_TYPE warriorTargetType;

    public HealthController healthController;

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

    public abstract TurnController GetOpponentTurnController();

    public void AddBeforePassiveController(PassiveController passiveController)
    {
        beforePassiveControllers.Add(passiveController);
    }

    public void AddDuringPassiveContrller(PassiveController passiveController)
    {
        duringPassiveControllers.Add(passiveController);
    }

    public void AddAfterPassiveController(PassiveController passiveController)
    {
        afterPassiveControllers.Add(passiveController);
    }

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
        Debug.Log("Starting Pre Turn");
        StartCoroutine(PreTurn());
    }

    private IEnumerator PreTurn()
    {

        Debug.Log("Getting text controller");
        TurnIndicatorTextController.GetInstance().PopInText(warriorTargetType == Warrior.TARGET_TYPE.ENEMY ? "YOUR TURN" : "DEMON'S TURN");

        Debug.Log("Popping in text");

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(ProcessControllers(beforePassiveControllers));

        yield return StartCoroutine(DealCards());

        turnState = TURN_STATE.ACTIVE;
    }

    public virtual void Update()
    {

    }

    public void DealCard()
    {
        GameObject newCard = CardListController.GetInstance().GetRandomCardObject();
        CardController newCardController = newCard.GetComponent<CardController>();
        newCardController.interactable = warriorTargetType == Warrior.TARGET_TYPE.ENEMY;

        currentHand.Add(newCardController);
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

            List<GameObject> prefabsForCard = PassivePrefabFetcher.GetInstance().FetchPassiveForCard(sacrificeController.card);

            foreach(GameObject passiveControllerPrefab in prefabsForCard)
            {
                GameObject passiveControllerObj = GameObject.Instantiate(passiveControllerPrefab, parent: passiveTransform);

                PassiveController passiveController = passiveControllerObj.GetComponent<PassiveController>();
                passiveController.SetCard(sacrificeController.card);

                TurnController turnController = passiveController.TargetsEnemy() ? GetOpponentTurnController() : this;

                passiveController.SetTurnController(this);

                switch (passiveController.passiveApplyPhase)
                {
                    case PassiveController.PASSIVE_APPLY_PHASE.BEFORE:
                        turnController.AddBeforePassiveController(passiveController);
                        break;

                    case PassiveController.PASSIVE_APPLY_PHASE.DURING:
                        turnController.AddDuringPassiveContrller(passiveController);
                        break;

                    case PassiveController.PASSIVE_APPLY_PHASE.AFTER:
                        turnController.AddAfterPassiveController(passiveController);
                        break;
                }
            }

            sacrificeController.Despawn();
            warriorSpawner.SpawnSacrifice();

            yield return new WaitForSeconds(.25f);
        }

        yield return new WaitForSeconds(1f);

        AltarController.GetInstance().BeginGlow();

        SacrificeController[] sacrifices = GameObject.FindObjectsOfType<SacrificeController>();

        yield return new WaitForSeconds(1f);

        foreach (SacrificeController sacrificeController in sacrifices) {
            sacrificeController.Shrug();
        }

        yield return new WaitForSeconds(2f);

        FireballController.GetInstance().Burst();
        CameraManager.GetInstance().ScreenShake(.05f, .5f, 500f);

        foreach (SacrificeController sacrificeController in sacrifices)
        {
            sacrificeController.Kill();
        }

        yield return new WaitForSeconds(2f);

        AltarController.GetInstance().EndGlow();

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator ProcessWarriors(List<CardController> warriorControllers)
    {

        List<Warrior> warriors = new List<Warrior>();

        foreach (CardController warriorController in warriorControllers)
        {
            Warrior warrior = warriorSpawner.SpawnWarrior(warriorController.combatPower, warriorTargetType);

            warriorController.Despawn();

            warriors.Add(warrior);

            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(.5f);

        yield return StartCoroutine(ProcessControllers(duringPassiveControllers));

        foreach (Warrior warrior in warriors)
        {

            warrior.Charge();

            yield return new WaitForSeconds(.5f);

        }

        yield return new WaitForSeconds(1f);
    }

    public void CheckForControllerExpiration(List<PassiveController> passiveControllers)
    {
        List<PassiveController> controllersToDelete = new List<PassiveController>();

        for(int i = 0; i < passiveControllers.Count; i++)
        {
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

    private IEnumerator ProcessControllers(List<PassiveController> passiveControllers)
    {
        foreach (PassiveController passiveController in passiveControllers)
        {
            passiveController.Process();

            if(!passiveController.Finished())
            {
                passiveController.ShowPassive();
            }

            yield return new WaitForSeconds(2f);

            passiveController.HidePassive();
        }
    }

    public IEnumerator Turn()
    {
        List<CardController> sacrificeControllers = currentHand.FindAll(card => card.GetDestiny() == CardController.CARD_DESTINY.SACRIFICE);
        List<CardController> warriorControllers = currentHand.FindAll(card => card.GetDestiny() == CardController.CARD_DESTINY.WARRIOR);

        currentHand.ForEach(card => card.interactable = false); 

        if(sacrificeControllers.Count > 0)
        {
            TurnIndicatorTextController.GetInstance().PopInText("SACRIFICE");

            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(ProcessSacrifices(sacrificeControllers));
        }

        TurnIndicatorTextController.GetInstance().PopInText("ATTACK");

        yield return new WaitForSeconds(4f);

        yield return StartCoroutine(ProcessWarriors(warriorControllers));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(ProcessControllers(afterPassiveControllers));


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
