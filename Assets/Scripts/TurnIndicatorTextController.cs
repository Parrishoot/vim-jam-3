using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnIndicatorTextController : Singleton<TurnIndicatorTextController>
{
    public TextMeshProUGUI text;

    public Animator animator;

    public void PopInText(string newText)
    {
        Debug.Log("Setting text!");
        text.text = newText;
        Debug.Log("Popping In!");
        animator.SetTrigger("popIn");
        Debug.Log("All Done!");
    }

    public void PopInAndStayText(string newText)
    {
        text.text = newText;
        animator.SetTrigger("popInAndStay");
    }
}
