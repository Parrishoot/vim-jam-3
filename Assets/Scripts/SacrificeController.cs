using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Animator animator;

    public void Shrug()
    {
        animator.SetTrigger("shrug");
        particleSystem.Play();
    }

    public void Kill()
    {
        particleSystem.Stop();
        Destroy(gameObject, .2f);
    }
}
