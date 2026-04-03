using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private Collider2D checkGround;
    [SerializeField] private Animator animJump;
    private Rigidbody2D rb;
    
    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        Debug.DrawRay(checkGround.transform.position, Vector2.down * 1.5f, Color.red);
        if(Input.GetButtonDown("Jump") && GetIsGrounded())
        {
            Jump();
        }
        
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private bool GetIsGrounded()
    {
        return Physics2D.Raycast(checkGround.transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
    }
}
