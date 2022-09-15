using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    private float currentShakeAmount = 0f;
    private float currentShakeSpeed = 0f;

    private float currentShakeTime = -1f;
    private bool shaking = false;

    private Vector3 startingPosition;

    public void Update()
    {
        if (shaking)
        {
            currentShakeTime -= Time.deltaTime;
            if (currentShakeTime <= 0)
            {
                shaking = false;
                transform.position = startingPosition;
            }
            else
            {
                gameObject.transform.position = new Vector3(startingPosition.x + Mathf.Sin(Time.time * currentShakeSpeed) * currentShakeAmount,
                                                            startingPosition.y + Mathf.Cos(Time.time * currentShakeSpeed) * currentShakeAmount,
                                                            startingPosition.z);
            }
        }
    }

    public void SetShake(float shakeAmount, float shakeTime, float shakeSpeed)
    {
        currentShakeTime = shakeTime;
        currentShakeAmount = shakeAmount;
        currentShakeSpeed = shakeSpeed;

        startingPosition = gameObject.transform.position;

        shaking = true;
    }

    public bool IsShaking()
    {
        return shaking;
    }
}
