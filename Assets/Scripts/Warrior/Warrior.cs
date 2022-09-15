using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public float chargeSpeed = 1.5f;

    public AnimationCurve chargeCurve;

    private Animator animator;

    public enum TARGET_TYPE
    {
        PLAYER,
        ENEMY
    }

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

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(int combatPower, TARGET_TYPE targetType)
    {
        this.combatPower = combatPower;

        target = targetType == TARGET_TYPE.PLAYER ? GameController.GetInstance().GetPlayerHealthController() : GameController.GetInstance().GetEnemyHealthController();

        startingLocation = transform.position;

        transform.localScale = new Vector3(Mathf.Sign(target.gameObject.transform.position.x - transform.position.x), transform.localScale.y, transform.localScale.z);

        
    }

    public void AddDamageModifier(float damageModifier)
    {

        if(damageModifier > 0)
        {
            gameObject.transform.localScale = new Vector3(1.5f * (Mathf.Sign(gameObject.transform.localScale.x)), 1.5f, 1);
        }

        combatPower = (int) (combatPower * damageModifier);
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
        animator.SetTrigger("charge");
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
