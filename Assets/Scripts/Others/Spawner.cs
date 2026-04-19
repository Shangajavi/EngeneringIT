using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxSpawn = 10;

    private float spawnerTimer;
    private int number;
    [SerializeField]private int alive;
    private bool isSpawning;
    private bool finishing;

    public event Action OnFinished;

    private void OnEnable()
    {
        spawnerTimer = spawnRate;
        number = 0;
        alive = 0;
        isSpawning = false;
        finishing = false;
    }

    private void Update()
    {
        StartSpawning();
        SpawnTime();
    }
    

    private void SpawnTime()
    {
        if (!isSpawning) return;
        if (number >= maxSpawn) return;

        spawnerTimer -= Time.deltaTime;

        if (spawnerTimer <= 0f)
        {
            spawnerTimer = spawnRate;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        number++;
        alive++;

        EnemyOne enemy = obj.GetComponent<EnemyOne>();
        if (enemy != null)
        {
            enemy.OnDeath += OnEnemyDeath;
        }
    }
    
    public void StartSpawning()
    {
        if (finishing) return;
        isSpawning = true;
    }


    private void OnEnemyDeath()
    {
        alive--;

        if (number >= maxSpawn && alive <= 0 && !finishing)
        {
            finishing = true;
            isSpawning = false;
            StartCoroutine(FinishAfterDelay(1f));
        }
    }

    private IEnumerator FinishAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnFinished?.Invoke();
    }
    
    private void OnDisable()
    {
        OnFinished = null;
    }

}