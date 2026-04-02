using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private bool isThere = false;
    private Rigidbody2D rb;
    private float hinput;
    private float jumpinput;
    private Vector2 Movement;
    
     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.position = GameManager.instance.SavedPosition;
        transform.eulerAngles = GameManager.instance.SavedOrientation;
    }

    // Update is called once per frame
    void Update()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        jumpinput = Input.GetAxisRaw("Jump");
        Movement = new Vector2(hinput, jumpinput);
        if(Input.GetKeyDown(KeyCode.E))
        {
            Collider2D result = Physics2D.OverlapCircle(transform.position, 2);
            if (result != null)
                {
                    if (result.gameObject.CompareTag("true"))
                    {
                        Destroy(result.gameObject);
                    }
                }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Movement * force,  ForceMode2D.Impulse);
        
    }


}
