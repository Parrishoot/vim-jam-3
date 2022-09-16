using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldAmount = 0f;

    bool active = true;

    public bool IsDepleted()
    {
        return active == false;
    }

    public int BlockDamage(int damage)
    {

        if(!active)
        {
            return damage;
        }

        shieldAmount -= damage;

        if(shieldAmount <= 0)
        {
            Debug.Log("Broke!");

            active = false;
            return (int) Mathf.Abs(shieldAmount);
        }

        return 0;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
