using System;
using System.Collections;
using UnityEngine;



public class EnemyOne : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 50;
  
    [SerializeField] private int moveSpeed = 2;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Path currentPath;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private int prize = 10;
    private Collider2D col2D;
    [SerializeField] private Collider2D attack;
    private SpriteRenderer sRenderer;
    private AudioSource audioSound;
    private int currentPoint;
    private  IPrizable prizable;
    private bool isAttacking = true;
    public event Action OnDeath;

    private void Awake()
    {
        
        prizable = FindFirstObjectByType<Info>(); 
        currentPath = GameObject.Find("Path").GetComponent<Path>();
        audioSound = GetComponent<AudioSource>();
        col2D = GetComponent<Collider2D>();
        sRenderer = GetComponent<SpriteRenderer>();
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
                if (isAttacking == true)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        

        if (health <= 0)
        { 
            StartCoroutine(Death());
        }
    }
    
    private IEnumerator Death()
    {
       col2D.enabled = false;
       sRenderer.enabled = false;
       
       if (prizable != null)
       {
           prizable.GiveMoney(prize);
       }       
       else
       {
           Debug.LogWarning("No se encontró ningún IPrizable (Info) en la escena.");
       }

       audioSound.PlayOneShot(deathSound);
       yield return new WaitForSeconds(deathSound.length);
       OnDeath?.Invoke();
       Destroy(gameObject);
    }

    private IEnumerator Attack()
    {
        
        isAttacking = false;
        yield return new WaitForSeconds(2f);
        audioSound.PlayOneShot(hitSound);
        
        attack.enabled = true;   
        yield return new WaitForSeconds(0.5f);
        attack.enabled = false;  

        isAttacking = true;

        
        
    }


}


