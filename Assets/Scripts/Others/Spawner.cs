using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField]private float spawnerTimer;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int  number;
    [SerializeField] private int maxSpawn;
    private bool isSpawning = true;
    
    private void Update()
    {
        spawnerTimer -= Time.deltaTime;
        if (spawnerTimer <= 0 && isSpawning == true)
        {
            spawnerTimer = spawnRate;
            SpawnEnemy();
        }

        if (number == maxSpawn)
        {
            isSpawning = false;
        }
    }

    public void SpawnEnemy()
    {
        GameObject spawnObject = Instantiate(Prefab);
        spawnObject.transform.position = transform.position;
        number++;
    }
}
