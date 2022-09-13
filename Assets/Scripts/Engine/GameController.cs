using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    public HealthController playerHealthController;
    public HealthController enemyHealthController;

    public HealthController GetPlayerHealthController()
    {
        return playerHealthController;
    }

    public HealthController GetEnemyHealthController()
    {
        return enemyHealthController;
    }
}
