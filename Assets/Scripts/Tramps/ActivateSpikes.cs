using System;
using UnityEngine;

public class ActivateSpikes : MonoBehaviour
{
    private SpriteRenderer box;

    private void Awake()
    {
        box = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            box.enabled = false;
            Info.Instance.BuildSpikes();
        }
    }
}
