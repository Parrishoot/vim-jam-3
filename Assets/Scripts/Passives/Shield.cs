using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldAmount = 0f;
    public Animator animator;
    public Shaker shaker;

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
        shaker.SetShake(.05f, .2f, 100f);

        if (shieldAmount <= 0)
        {
            animator.SetTrigger("break");
            active = false;
            return (int) Mathf.Abs(shieldAmount);
        }

        return 0;
    }

    public void Despawn()
    {
        animator.SetTrigger("break");
    }
}
