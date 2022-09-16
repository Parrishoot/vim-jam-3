using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PassiveShower : MonoBehaviour
{

    public bool leftSide = true;
    public bool showing = false;

    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Now this is what I call "Game Jam Code"
        float LEFT_BOUNDS = Screen.width * .25f;
        float RIGHT_BOUNDS = Screen.width * .75f;

        if (Input.mousePosition.x < LEFT_BOUNDS && leftSide && !showing)
        {
            showing = true;
            animator.SetTrigger("moveRight");
        }
        else if(Input.mousePosition.x > RIGHT_BOUNDS && !leftSide && !showing)
        {
            showing = true;
            animator.SetTrigger("moveLeft");
        }
        else if(showing && Input.mousePosition.x >= LEFT_BOUNDS && Input.mousePosition.x <= RIGHT_BOUNDS)
        {
            showing = false;
            animator.SetTrigger(leftSide ? "moveLeft" : "moveRight");
        }
    }

}
