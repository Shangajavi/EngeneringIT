using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float length, startPoint;

    [SerializeField] private GameObject cam1;
    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        startPoint = cam1.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; 
    }

    private void Update()
    {
        float temp = (cam1.transform.position.x *(1-parallaxEffect));
        float dist = (cam1.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPoint + dist, transform.position.y, transform.position.z);
        if (temp > startPoint + length)
        {
            startPoint += length;
        }
        else if (temp < startPoint - length)
        {
            startPoint -= length;
        }
    }
}
