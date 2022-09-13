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

    public void Damage(int damage)
    {
        totalHealth -= damage;
    }
}
