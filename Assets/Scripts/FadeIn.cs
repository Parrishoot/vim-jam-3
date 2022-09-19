using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetTrigger("fadeIn");
    }
}
