using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animMove;
    
    [SerializeField] private GameObject camClose;
    [SerializeField] private GameObject camAway;
    
    private SpriteRenderer sRenderer;
    private Rigidbody2D rb;
    private float jumpinput;
    private float hinput;
    private Vector2 movement;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        camAway.SetActive(false);
        camClose.SetActive(true);
        
    }

    void Update()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        jumpinput = Input.GetAxisRaw("Jump");
        movement = new Vector2(hinput, jumpinput * jumpForce);
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            camClose.SetActive(!camClose.activeSelf);
            camAway.SetActive(!camAway.activeSelf);

        }
        Animations();
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * movementForce, ForceMode2D.Impulse);
    }

    private void Animations()
    {
        
        if (hinput != 0 )
        {
            animMove.SetBool("isMoving", true);
            if (hinput > 0)
            {
                sRenderer.flipX = false;
            }
            else if (hinput < 0)
            {
               sRenderer.flipX = true; 
            }
        }
        
        else
        {
            animMove.SetBool("isMoving", false);
            
        }
    }
}
