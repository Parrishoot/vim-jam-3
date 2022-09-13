using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSpawner : Singleton<WarriorSpawner>
{
    public float spawnRadius = 3f;

    public Warrior SpawnWarrior(GameObject warriorPrefab, int combatPower)
    {
        Vector2 randomLocation = Random.insideUnitCircle * spawnRadius;
        Vector2 spawnLocation = gameObject.transform.position + new Vector3(randomLocation.x, randomLocation.y, 0f);

        GameObject obj = GameObject.Instantiate(warriorPrefab, spawnLocation, Quaternion.identity);
        obj.GetComponent<Warrior>().Init(combatPower);

        return obj.GetComponent<Warrior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
