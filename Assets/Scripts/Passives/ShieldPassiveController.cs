using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPassiveController : PassiveController
{
    public GameObject shieldPrefab;

    private Shield shield;

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

        base.Process();

        if (Finished() || shield.IsDepleted())
        {
            shield.Despawn();
        }
    }
}
