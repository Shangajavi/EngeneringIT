using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MovementForce;
    [SerializeField] private float jumpForce;
    
    [SerializeField] private GameObject CamClose;
    [SerializeField] private GameObject CamAway;
    
    private Rigidbody2D rb;
    private float jumpinput;
    private float hinput;
    private Vector2 Movement;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CamAway.SetActive(false);
        CamClose.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            CamClose.SetActive(!CamClose.activeSelf);
            CamAway.SetActive(!CamAway.activeSelf);

        }
        hinput = Input.GetAxisRaw("Horizontal");
        jumpinput = Input.GetAxisRaw("Jump");
        Movement = new Vector2(hinput, jumpinput * jumpForce);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Movement * MovementForce, ForceMode2D.Impulse);
    }
}
