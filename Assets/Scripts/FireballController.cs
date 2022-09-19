using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : Singleton<FireballController>
{
    public AudioSource audioSource;
    public Animator animator;

    public void Burst()
    {
        animator.SetTrigger("burst");
        audioSource.Play();
    }

}
