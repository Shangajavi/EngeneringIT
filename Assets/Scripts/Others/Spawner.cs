using System;
using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int maxSpawn = 10;

    private float spawnerTimer;
    private int number;
    private bool isSpawning;
    private bool finishing; // evita disparar la corutina más de una vez

    public event Action OnFinished;

    private void OnEnable()
    {
        spawnerTimer = spawnRate;
        number = 0;
        isSpawning = true;     // ✅ IMPORTANTE: debe ser TRUE
        finishing = false;
    }

    private void Update()
    {
        SpawnTime();
    }

    private void SpawnTime()
    {
        if (!isSpawning) return;

        spawnerTimer -= Time.deltaTime;

        if (spawnerTimer <= 0f)
        {
            spawnerTimer = spawnRate;
            SpawnEnemy();
        }

        if (number >= maxSpawn && !finishing)
        {
            finishing = true;
            isSpawning = false;
            StartCoroutine(FinishAfterDelay(1f));
        }
    }

    private void SpawnEnemy()
    {
        if (Prefab == null) return;

        Instantiate(Prefab, transform.position, Quaternion.identity);
        number++;
    }

    private IEnumerator FinishAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnFinished?.Invoke();
    }
}

