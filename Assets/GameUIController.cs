using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    public void SetText(string newTitleText)
    {
        titleText.SetText(newTitleText);
    }
}
