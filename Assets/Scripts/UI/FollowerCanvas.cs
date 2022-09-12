using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCanvas : Singleton<FollowerCanvas>
{
    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
