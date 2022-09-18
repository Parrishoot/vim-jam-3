using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSpawner : Singleton<WarriorSpawner>
{
    public float spawnRadius = 3f;

    public GameObject warriorPrefab;
    public GameObject sacrificePrefab;


    private Vector3 GetRandomLocation()
    {
        Vector2 randomLocation = Random.insideUnitCircle * spawnRadius;
        return gameObject.transform.position + new Vector3(randomLocation.x, randomLocation.y, 0f);
    }

    public Warrior SpawnWarrior(int combatPower, Warrior.TARGET_TYPE targetType)
    {
        Vector3 spawnLocation = GetRandomLocation();

        GameObject obj = GameObject.Instantiate(warriorPrefab, spawnLocation, Quaternion.identity);

        obj.GetComponent<Warrior>().Init(combatPower, targetType);

        return obj.GetComponent<Warrior>();
    }

    public void SpawnSacrifice()
    {
        Vector3 spawnLocation = GetRandomLocation();

        GameObject.Instantiate(sacrificePrefab, spawnLocation, Quaternion.identity);
    }
}
