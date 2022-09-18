using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public void SetText(int health)
    {
        healthText.SetText(health.ToString());
    }
}
