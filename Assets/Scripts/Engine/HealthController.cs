using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public HealthUIController healthUIController;
    public Transform warriorTargetTransform;
    public AudioSource oofAudioSource;
    public AudioSource hitAudioSource;

    public int totalHealth = 50;

    public void Start()
    {

    }

    public bool IsDead()
    {
        return totalHealth <= 0;
    }

    public void Update()
    {
        healthUIController.SetText(totalHealth);
    }

    public int Damage(int damage)
    {

        int remainingDamage = damage;

        foreach(Shield shield in GetComponentsInChildren<Shield>())
        {
            remainingDamage = shield.BlockDamage(remainingDamage);
        }

        if(remainingDamage > 0)
        {
            totalHealth -= remainingDamage;

            healthUIController.PlayHurtParticles();
            gameObject.GetComponent<Shaker>().SetShake(.05f, .2f, 100f);
            AudioUtils.PlaySoundWithRandomPitch(oofAudioSource, .05f, 1f);
            AudioUtils.PlaySoundWithRandomPitch(hitAudioSource, .05f, 1f);
        }

        if(IsDead())
        {
            GameController.GetInstance().CheckForGameOver();
        }

        return remainingDamage;
    }

    public void Heal(int healAmount)
    {
        healthUIController.PlayHealParticles();
        totalHealth += healAmount;
    }
}
