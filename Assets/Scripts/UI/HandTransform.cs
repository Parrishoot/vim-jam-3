using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTransform : Singleton<HandTransform>
{
    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
