using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class DraggableElement : UIFollower, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor;
    public float scaleSpeed;

    public float rotateAmount = 30f;
    public float rotateSpeed = 3f;

    public AnimationCurve curve;

    private bool onCard = false;

    private float lerpTime = 0f;

    private Vector3 targetScale;
    private Vector3 startScale;

    private Vector3 initScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        startScale = transform.localScale;
        targetScale = scaleFactor * initScale;

        transform.SetAsLastSibling();

        onCard = true;

        lerpTime = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startScale = transform.localScale;
        targetScale = initScale;

        onCard = false;

        transform.eulerAngles = Vector3.zero;

        lerpTime = 0f;
    }

    public override void Start()
    {

        initScale = transform.localScale;
        targetScale = transform.localScale;

        base.Start();

        // Move everything in front
        transform.position += new Vector3(0, 0, -10);
    }


    public override void Update()
    {
        if (transform.localScale != targetScale)
        {
            Vector3 newScale = Vector3.Lerp(startScale, targetScale, curve.Evaluate(lerpTime));
            transform.localScale = newScale;

            lerpTime += Time.deltaTime * scaleSpeed;
        }

        // TODO: FIX THIS
        //if(onCard)
        //{
        //    Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    transform.eulerAngles = new Vector3(-(originPoint.transform.position.y - mousePos.y) * rotateFactor,
        //                                        (originPoint.transform.position.x - mousePos.x) * rotateFactor,
        //                                        0);
        //}

        if(onCard)
        {
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.Sin(Time.time * rotateSpeed) * rotateAmount);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        base.Update();
    }
}
