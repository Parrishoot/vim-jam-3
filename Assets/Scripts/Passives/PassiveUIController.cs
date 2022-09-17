using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PassiveUIController: UIFollower
{
    public TextMeshProUGUI passiveText;

    public void SetText(string passiveString)
    {
        passiveText.SetText(passiveString);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

}
