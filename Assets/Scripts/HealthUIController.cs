using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public ParticleSystem healParticleSystem;
    public ParticleSystem hurtParticleSystem;

    public void PlayHealParticles()
    {
        healParticleSystem.Play();
    }

    public void PlayHurtParticles()
    {
        hurtParticleSystem.Play();
    }

    public void SetText(int health)
    {
        healthText.SetText(health.ToString());
    }
}
