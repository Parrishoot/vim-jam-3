using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPassiveController : PassiveController
{
    public GameObject shieldPrefab;

    private Shield shield;

    public override string GetText()
    {
        float shieldAmount = 0f;

        if(shield != null)
        {
            shieldAmount = Mathf.Max(shield.shieldAmount, 0f);
        }
        else
        {
            shieldAmount = card.shieldAmount;
        }

        return StringUtils.GetShieldText(shieldAmount) + StringUtils.GetCardPostfix(turnCount);
    }

    public override bool Finished()
    {
        return base.Finished() || shield.IsDepleted();
    }

    public override void Process()
    {

        if (shield == null)
        {
            Transform parent = turnController.healthController.gameObject.transform;
            GameObject shieldObject = Instantiate(shieldPrefab);
            shieldObject.transform.SetParent(parent, false);

            shield = shieldObject.GetComponent<Shield>();
            shield.shieldAmount = card.shieldAmount;
        }

        turnCount -= 1;
    }
}
