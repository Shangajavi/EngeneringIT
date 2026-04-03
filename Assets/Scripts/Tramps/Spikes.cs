using System;
using System.Collections;
using UnityEngine;





public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject Up;
    [SerializeField] private GameObject Down;
    [SerializeField] private float timeDown;
    [SerializeField] private float timeUp;
    private bool IsDown = true;
    private Vector2 positionUP;
    private Vector2 positionDown;

    private void Awake()
    {
         positionUP = Up.transform.position;
         positionDown = Down.transform.position;
        this.gameObject.transform.position = new Vector3(0f, positionDown.y, 0f);
    }

    private void Update()
    {
        if (IsDown == true)
        {
            StartCoroutine(SpikeMovement());
        }
    }

    private IEnumerator SpikeMovement()
    { 
        IsDown = false;  
        this.gameObject.transform.position = new Vector3(0f, positionUP.y, 0f);
        yield return new  WaitForSeconds(timeUp);
        this.gameObject.transform.position = new Vector3(0f, positionDown.y, 0f);
        yield return new  WaitForSeconds(timeDown);
        IsDown = true;
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



