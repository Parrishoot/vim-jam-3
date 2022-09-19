using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnIndicatorTextController : Singleton<TurnIndicatorTextController>
{
    public TextMeshProUGUI text;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PopInText(string newText)
    {
        text.text = newText;
        animator.SetTrigger("popIn");
    }

    public void PopInAndStayText(string newText)
    {
        text.text = newText;
        animator.SetTrigger("popInAndStay");
    }
}
