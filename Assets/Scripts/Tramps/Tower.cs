using System;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float range = 4f;
    [SerializeField] private LayerMask enemyLayer;
    
    [SerializeField] private float fireCooldown = 0.6f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Bullet bulletPrefab;
    
    private float fireTimer;


    private Transform currentTarget;


    void Update()
    {
        FindTarget();
        
        if (currentTarget != null)
        {
            AimAtTarget();
        }

        if (currentTarget != null)
        {
            Debug.DrawLine(transform.position, currentTarget.position, Color.green);

        }
        TryShoot();
    }
    

    private void FindTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        if (enemiesInRange.Length == 0)
        {
            currentTarget = null;
            return;
        }

        Transform bestTarget = null;
        float bestDistance = Mathf.Infinity;

        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            float d = Vector2.Distance(transform.position, enemiesInRange[i].transform.position);
            if (d < bestDistance)
            {
                bestDistance = d;
                bestTarget = enemiesInRange[i].transform;
            }
        }

        currentTarget = bestTarget;
    }
    

    private void TryShoot()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            if (currentTarget != null)
            {
                Bullet b = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                b.Init(currentTarget);
            }

            fireTimer = fireCooldown;
        }
    }



    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
    private void AimAtTarget()
    {
        Vector2 direction = (currentTarget.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Si tu sprite mira hacia la derecha por defecto:
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }



}
