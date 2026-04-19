using System;
using UnityEngine;

public class GoBackPlayer : MonoBehaviour
{
    [SerializeField] private GameObject goBack;

    private void Awake()
    {
        goBack = FindFirstObjectByType(typeof(GameObject)) as GameObject;
        if (goBack == null)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoBack"))
        {
            this.transform.position = goBack.transform.position;
        }
    }
}
