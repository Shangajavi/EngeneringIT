using System;
using UnityEngine;



public class EnemyOne : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage;
    [SerializeField] private int moveSpeed = 2;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Path currentPath;
    private int currentPoint;

    private void Awake()
    {
        currentPath = GameObject.Find("Path").GetComponent<Path>();
    }

    private void OnEnable()
    {
        currentPoint = 0;
        targetPosition = currentPath.GetPosition(currentPoint);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        float distance = (transform.position - targetPosition).magnitude;
        if (distance <= 0.1f)
        {
            if (currentPoint < currentPath.wayPoints.Length - 1)
            {
                currentPoint++;
                targetPosition = currentPath.GetPosition(currentPoint);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemigo dañado");

        if (health <= 0)
            Destroy(gameObject);
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}


