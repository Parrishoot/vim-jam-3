using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameController : Singleton<RestartGameController>
{
    public void ShowRestartGamePrompt()
    {
        GetComponent<Animator>().SetTrigger("flyIn");
    }

    public void Start()
    {
        GetComponent<Animator>().SetTrigger("wait");
    }
}
