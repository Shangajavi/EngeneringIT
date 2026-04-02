using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField]private float spawnerTimer;
    [SerializeField] private GameObject Prefab;
    
    private void Update()
    {
        spawnerTimer -= Time.deltaTime;
        if (spawnerTimer <= 0)
        {
            spawnerTimer = spawnRate;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject spawnObject = Instantiate(Prefab);
        spawnObject.transform.position = transform.position;
    }
}
