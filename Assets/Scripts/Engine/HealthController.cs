using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public HealthUIController healthUIController;
    public Transform warriorTargetTransform;

    public int totalHealth = 50;

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
