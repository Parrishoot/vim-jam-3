using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollower : MonoBehaviour
{
    public GameObject parentObject;

    public float followSpeed = 40f;

    public virtual void Start()
    {
        transform.SetParent(FollowerCanvas.GetInstance().GetTransform(), true);
    }

    public virtual void Update()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, parentObject.transform.position, Mathf.SmoothStep(0f, 1f, Time.deltaTime * followSpeed));
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        
    }
}
