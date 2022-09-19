using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public float chargeSpeed = 1.5f;

    public AnimationCurve chargeCurve;

    private Animator animator;
    private AudioSource audioSource;

    public AudioClip spawnAudio;
    public AudioClip punchAudio;

    public float lerpSpeed;

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

    private Vector3 startingScale;

    public void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = spawnAudio;
        AudioUtils.PlaySoundWithRandomPitch(audioSource, .05f, 1);
    }

    public void Init(int combatPower, TARGET_TYPE targetType)
    {
        this.combatPower = combatPower;

        target = targetType == TARGET_TYPE.PLAYER ? GameController.GetInstance().GetPlayerHealthController() : GameController.GetInstance().GetEnemyHealthController();

        startingLocation = transform.position;

        transform.localScale = new Vector3(Mathf.Sign(target.gameObject.transform.position.x - transform.position.x), transform.localScale.y, transform.localScale.z);

        startingScale = transform.localScale;

        transform.localScale = GetTargetScale();
    }

    public void AddDamageModifier(float damageModifier)
    {
        combatPower = (int) (combatPower * damageModifier);
    }

    public void Update()
    {
        switch(warriorState)
        {
            case WARRIOR_STATE.CHARGING:

                if(currentChargeTime <= 1f)
                {
                    transform.position = Vector3.Lerp(startingLocation, target.warriorTargetTransform.position, chargeCurve.Evaluate(currentChargeTime));

                    currentChargeTime += Time.deltaTime * chargeSpeed;
                }
                else
                {
                    warriorState = WARRIOR_STATE.ATTACKING;
                }
                break;

            case WARRIOR_STATE.ATTACKING:
                target.Damage(combatPower);
                
                animator.SetTrigger("punch");

                GetComponent<Shaker>().SetShake(.05f, .2f, 500);
                warriorState = WARRIOR_STATE.DESPAWNING;
                break;

            case WARRIOR_STATE.DESPAWNING:
                break;
        }

        Vector3 targetScale = GetTargetScale();
        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Mathf.SmoothStep(0f, 1f, Time.deltaTime * lerpSpeed));
        }
    }

    public void Charge()
    {
        animator.SetTrigger("charge");
        warriorState = WARRIOR_STATE.CHARGING;
    }

    public Vector3 GetTargetScale()
    {
        return startingScale * (1 + Mathf.Min(combatPower / 15f, 4f));
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
