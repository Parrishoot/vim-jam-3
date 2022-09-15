using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private Shaker shaker;

    private void Start()
    {
        shaker = GetComponent<Shaker>();
    }

    public void ScreenShake(float shakeAmount, float shakeTime, float shakeSpeed)
    {
        shaker.SetShake(shakeAmount, shakeTime, shakeSpeed);
    }
}
