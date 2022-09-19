using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    public new Light2D light;

    public float flickerTime = .1f;
    public float flickerLowerBounds = .7f;
    public float flickerHigherBounds = 1.2f;

    float currentFlickerTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(currentFlickerTime <= 0f)
        {
            light.intensity = Random.Range(flickerLowerBounds, flickerHigherBounds);
            currentFlickerTime = flickerTime;
        }

        currentFlickerTime -= Time.deltaTime;
    }
}
