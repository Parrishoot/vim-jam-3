using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    public HealthController playerHealthController;
    public HealthController enemyHealthController;

    public TurnController playerTurnController;
    public TurnController enemyTurnController;

    private TurnController activeTurnController;

    public HealthController GetPlayerHealthController()
    {
        return playerHealthController;
    }

    public HealthController GetEnemyHealthController()
    {
        return enemyHealthController;
    }
    public void Start()
    {
        activeTurnController = playerTurnController;
        playerTurnController.StartTurn();
    }

    public void SwitchTurns()
    {
        if(activeTurnController == playerTurnController)
        {
            // TODO: CHANGE THIS
            activeTurnController = enemyTurnController;
        }
        else
        {
            activeTurnController = playerTurnController;
        }

        activeTurnController.StartTurn();
    }
}
