using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;

    private Transform target;
    
    // La torre llamará a esto cuando dispare
    public void Init(Transform newTarget)
    {
        target = newTarget;
        Destroy(gameObject, lifeTime); // por si no golpea a nadie
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            
            damageable.TakeDamage(damage);
            Destroy(gameObject); // IMPORTANTÍSIMO: que no pegue varias veces
        }
    }
}
