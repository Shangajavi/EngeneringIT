using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField]private float spawnerTimer;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int  number;
    [SerializeField] private int maxSpawn;
    [SerializeField]private bool isSpawning = true;
    
    private void Update()
    {
        SpawnTime();
        

    }

    public void SpawnEnemy()
    {
        GameObject spawnObject = Instantiate(Prefab);
        spawnObject.transform.position = transform.position;
        number++;
    }
    
    private void SpawnTime()
    {
        if (isSpawning == false)
        {
            return;
        }

        spawnerTimer -= Time.deltaTime;

        if (spawnerTimer <= 0f)
        {
            spawnerTimer = spawnRate;
            SpawnEnemy();
        }

        if (number >= maxSpawn)
        {
            isSpawning = false;
        }
    }

}
