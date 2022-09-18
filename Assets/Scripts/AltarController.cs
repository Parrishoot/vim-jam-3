using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : Singleton<AltarController>
{
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginGlow()
    {
        animator.SetTrigger("beginGlow");
    }

    public void EndGlow()
    {
        animator.SetTrigger("endGlow");
    }
}
