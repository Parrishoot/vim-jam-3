using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldAmount = 0f;
    public Animator animator;
    public Shaker shaker;

    public AudioSource audioSource;

    public AudioClip breakClip;
    public AudioClip hitClip;

    bool active = true;

    public void Start()
    {
        audioSource.Play();
    }

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
            audioSource.clip = breakClip;
            audioSource.Play();
            active = false;
            return (int) Mathf.Abs(shieldAmount);
        }
        else
        {
            audioSource.clip = hitClip;
            audioSource.Play();
        }

        return 0;
    }

    public void Despawn()
    {
        animator.SetTrigger("break");
    }
}
