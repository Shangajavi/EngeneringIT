using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class EnemyPatrolSystem : MonoBehaviour
{
    [SerializeField] private Transform patrolPath;
    [SerializeField] private int patrolSpeed;
    private List<Vector3> patrolPositions = new();
    private int Idexdestination;
    private Vector3 CurrentDestination;
    //private Animator anim;


    private void Awake()
    {
        //anim = GetComponent<Animator>();
        foreach (Transform patrolPoint in patrolPath)
        {
            patrolPositions.Add(patrolPoint.position);
        }
        StartCoroutine(PatrolAndWait()); 

    }

    private IEnumerator PatrolAndWait()
    {
        
        while (true)
        {
            //anim.SetBool("isWalking", true);
            while (transform.position != CurrentDestination) //mientras no hayas llegado...
            {
                calculateNewDestination();
                transform.position =
                    Vector3.MoveTowards(transform.position, CurrentDestination, patrolSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            //anim.SetBool("isWalking", false);
            yield return new WaitForSeconds(2);
            Idexdestination = (Idexdestination + 1) % patrolPositions.Count; // 23 horas + 2 = 1 horas % lo limita a ese valor max

        }
    }

    private void FaceToDestinatio()
    {
        float x = CurrentDestination.x - transform.position.x;
        if (Mathf.Sign(x) == -1) // Da 1 o -1
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Mathf.Sign(x) == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void calculateNewDestination()
    {
        CurrentDestination = patrolPositions[Idexdestination];
    }


}
