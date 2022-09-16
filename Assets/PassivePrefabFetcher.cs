using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePrefabFetcher : Singleton<PassivePrefabFetcher>
{

    public GameObject damageModifierPassivePrefab;
    public GameObject shieldPassivePrefab;
    public GameObject healPassivePrefab;
    public GameObject lifeStealPassivePrefab;
    public GameObject handSizeAdjustPassivePrefab;

    public List<GameObject> FetchPassiveForCard(Card card)
    {
        List<GameObject> passivePrefabsForCard = new List<GameObject>();

        if(card.damageModifier != 1)
        {
            passivePrefabsForCard.Add(damageModifierPassivePrefab);
        }

        if(card.shieldAmount != 0)
        {
            passivePrefabsForCard.Add(shieldPassivePrefab);
        }

        if(card.healAmount != 0)
        {
            passivePrefabsForCard.Add(healPassivePrefab);
        }

        if(card.stealAmount != 0)
        {
            passivePrefabsForCard.Add(lifeStealPassivePrefab);
        }

        if(card.handSizeAdjustAmount != 0)
        {
            passivePrefabsForCard.Add(handSizeAdjustPassivePrefab);
        }

        return passivePrefabsForCard;
    }

}
