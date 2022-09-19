using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveUIController: UIFollower
{
    public TextMeshProUGUI passiveText;
    public Animator animator;
    public AudioSource audioSource;

    public void SetBorderActive()
    {
        animator.SetTrigger("passiveActive");
        AudioUtils.PlaySoundWithRandomPitch(audioSource, .2f, 1f);
    }

    public void SetBorderInactive()
    {
        animator.SetTrigger("passiveInactive");
    }

    public void SetText(string passiveString)
    {
        passiveText.SetText(passiveString);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

}
