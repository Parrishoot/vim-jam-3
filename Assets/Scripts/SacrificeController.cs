using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeController : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    public Animator animator;
    public AudioSource audioSource;

    public void Start()
    {
        AudioUtils.PlaySoundWithRandomPitch(audioSource, .05f, 1);
    }

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
