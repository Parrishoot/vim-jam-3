using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSpawner : Singleton<WarriorSpawner>
{
    public float spawnRadius = 3f;

    public GameObject warriorPrefab;
    public GameObject sacrificePrefab;

    public BoxCollider2D boundsCollider;

    private Bounds bounds;

    public void Start()
    {
        bounds = boundsCollider.bounds;
    }

    private Vector3 GetRandomLocation()
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0f
        );
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
