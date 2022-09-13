using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public float chargeSpeed = 1.5f;

    public AnimationCurve chargeCurve;

    public enum TARGET_TYPE
    {
        PLAYER,
        ENEMY
    }

    public TARGET_TYPE targetType = TARGET_TYPE.ENEMY;

    private enum WARRIOR_STATE
    {
        PENDING,
        CHARGING,
        ATTACKING,
        DESPAWNING
    }

    private WARRIOR_STATE warriorState = WARRIOR_STATE.PENDING; 
    private int combatPower;

    private HealthController target;

    private float currentChargeTime;
    private Vector3 startingLocation;

    public void Init(int combatPower)
    {
        this.combatPower = combatPower;

        target = targetType == TARGET_TYPE.PLAYER ? GameController.GetInstance().GetPlayerHealthController() : GameController.GetInstance().GetEnemyHealthController();

        startingLocation = transform.position;
    }

    public void Update()
    {
        switch(warriorState)
        {
            case WARRIOR_STATE.CHARGING:

                if(currentChargeTime <= 1f)
                {
                    transform.position = Vector3.Lerp(startingLocation, target.gameObject.transform.position, chargeCurve.Evaluate(currentChargeTime));

                    currentChargeTime += Time.deltaTime * chargeSpeed;
                }
                else
                {
                    warriorState = WARRIOR_STATE.ATTACKING;
                }
                break;

            case WARRIOR_STATE.ATTACKING:
                target.Damage(combatPower);
                Despawn();
                break;

            case WARRIOR_STATE.DESPAWNING:
                break;
            
        }
    }

    public void Charge()
    {
        warriorState = WARRIOR_STATE.CHARGING;
    }

    public bool PendingDespawn()
    {
        return warriorState == WARRIOR_STATE.DESPAWNING;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

}
