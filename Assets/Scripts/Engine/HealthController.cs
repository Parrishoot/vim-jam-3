using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int totalHealth = 50;

    public bool IsDead()
    {
        return totalHealth <= 0;
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

            gameObject.GetComponent<Shaker>().SetShake(.05f, .2f, 100f);
        }

        return remainingDamage;
    }

    public void Heal(int healAmount)
    {
        totalHealth += healAmount;
    }
}
