
using System;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] public GameObject[] wayPoints;
    private int i;

    public Vector3 GetPosition(int index)
    {
        return wayPoints[index].transform.position;
    }
    private void OnDrawGizmos()
    {
        if (wayPoints.Length > 0)
        {
            for(i = 0;  i < wayPoints.Length; i++)
            {
                if (i < wayPoints.Length - 1)
                {
                    Gizmos.color = Color.whiteSmoke;
                    Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[i + 1].transform.position);
                }
            }
        }
    }
}
