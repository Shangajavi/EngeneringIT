using UnityEngine;

public class Heart : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    public void TakeDamage(int amount)
    {
        health -= amount;
        

        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
    }

}
