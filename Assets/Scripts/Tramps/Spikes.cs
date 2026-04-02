using System;
using UnityEngine;




public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}



