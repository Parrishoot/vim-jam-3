using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public HealthUIController healthUIController;
    public Transform warriorTargetTransform;
    public AudioSource oofAudioSource;
    public AudioSource hitAudioSource;

    public int totalHealth = 50;

    public float fadeOutSpeed = .1f;

    public bool IsDead()
    {
        return totalHealth <= 0;
    }

    public void Update()
    {
        healthUIController.SetText(totalHealth);
    }

    public int Damage(int damage)
    {

        int remainingDamage = damage;

        foreach(Shield shield in GetComponentsInChildren<Shield>())
        {
            remainingDamage = shield.BlockDamage(remainingDamage);
        }

        if(remainingDamage > 0)
        {
            totalHealth -= remainingDamage;

            if (totalHealth > 0)
            {
                healthUIController.PlayHurtParticles();
                gameObject.GetComponent<Shaker>().SetShake(.05f, .2f, 100f);
                AudioUtils.PlaySoundWithRandomPitch(hitAudioSource, .05f, 1f);
            }
            else
            {
                gameObject.GetComponent<Shaker>().SetShake(.1f, 2f, 100f);
                GetComponentInChildren<Animator>().SetTrigger("die");

                StartCoroutine(Die());
            }

            AudioUtils.PlaySoundWithRandomPitch(oofAudioSource, .05f, 1f);

        }

        if(IsDead())
        {
            GameController.GetInstance().CheckForGameOver();
        }

        return remainingDamage;
    }

    public IEnumerator Die()
    {
        gameObject.GetComponent<Shaker>().SetShake(.1f, 2f, 100f);
        GetComponentInChildren<Animator>().SetTrigger("die");

        hitAudioSource.loop = true;
        hitAudioSource.Play();

        while (hitAudioSource.volume >= 0)
        {
            hitAudioSource.volume -= Mathf.Max(Time.deltaTime * fadeOutSpeed);
            yield return null;
        }

        hitAudioSource.Stop();
    }

    public void Heal(int healAmount)
    {
        healthUIController.PlayHealParticles();
        totalHealth += healAmount;
    }
}
