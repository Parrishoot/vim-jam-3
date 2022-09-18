using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    public HealthController playerHealthController;
    public HealthController enemyHealthController;

    public TurnController playerTurnController;
    public TurnController enemyTurnController;

    public GameObject GameOverUI;

    private GameUIController uiController;

    public enum GAME_STATE
    {
        MAIN_MENU,
        IN_PROGRESS,
        GAME_OVER
    }

    public GAME_STATE gameState;

    private TurnController activeTurnController;

    public void Start()
    {
        uiController = GameOverUI.GetComponent<GameUIController>();

        gameState = GAME_STATE.IN_PROGRESS;

        activeTurnController = playerTurnController;
        playerTurnController.StartTurn();
    }

    public HealthController GetPlayerHealthController()
    {
        return playerHealthController;
    }

    public HealthController GetEnemyHealthController()
    {
        return enemyHealthController;
    }


    public bool InProgress()
    {
        return gameState == GAME_STATE.IN_PROGRESS;
    }

    public bool IsGameOver()
    {
        return gameState == GAME_STATE.GAME_OVER;
    }

    public void CheckForGameOver()
    {
        if(InProgress())
        {
            if (playerTurnController.healthController.IsDead())
            {
                uiController.SetText("GAME OVER!");
                GameOverUI.SetActive(true);
                gameState = GAME_STATE.GAME_OVER;
            }
            else if (enemyTurnController.healthController.IsDead())
            {
                uiController.SetText("YOU WIN!");
                GameOverUI.SetActive(true);
                gameState = GAME_STATE.GAME_OVER;
            }
        }
    }

    public void SwitchTurns()
    {
        if(InProgress())
        {
            if (activeTurnController == playerTurnController)
            {
                activeTurnController = enemyTurnController;
            }
            else
            {
                activeTurnController = playerTurnController;
            }

            activeTurnController.StartTurn();
        }
    }
}
