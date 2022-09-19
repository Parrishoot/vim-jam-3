using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtils
{
    public static void PlaySoundWithRandomPitch(AudioSource source, float pitchRandomBounds, float basePitch = -1f)
    {
        if(basePitch == -1)
        {
            basePitch = source.pitch;
        }

        float newPitch = Random.Range(basePitch - (pitchRandomBounds * basePitch), basePitch + pitchRandomBounds * basePitch);

        source.pitch = newPitch;
        source.Play();
    }
}
