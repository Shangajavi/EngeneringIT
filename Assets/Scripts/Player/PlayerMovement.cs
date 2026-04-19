using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementForce;
    [SerializeField] private Animator animMove;
    
    [SerializeField] private GameObject camClose;
    [SerializeField] private GameObject camAway;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    private Vector2 movementInput; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
 
        if (camAway != null) camAway.SetActive(false);
        if (camClose != null) camClose.SetActive(true);

        
    }


    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnCamInitiated += CamSwitch;
    }



    void Update()
    {
         if (InputManager.Instance != null)
         {
             movementInput = InputManager.Instance.GetMovement();
         }
         else
         {
             movementInput = Vector2.zero;
         }
         MoveAnimations(movementInput.x);
         

        
    }
    
    private void FixedUpdate()
    {
        float h = movementInput.x;

        rb.AddForce(Vector2.right * (h * movementForce * 4), ForceMode2D.Force);

        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
        }
    }


    private void MoveAnimations(float h)
    {
        
        if (h != 0 )
        {
            animMove.SetBool("isMoving", true);
            if (h > 0)
            {
                sRenderer.flipX = false;
            }
            else if (h < 0)
            {
               sRenderer.flipX = true; 
            }
        }
        
        else
        {
            animMove.SetBool("isMoving", false);
            
        }
    }

    private void CamSwitch()
    {
        
        if (camClose == null || camAway == null) return;
        camClose.SetActive(!camClose.activeSelf);
        camAway.SetActive(!camAway.activeSelf);

    }
    private void OnDestroy()
    {
        
        if (InputManager.Instance != null)
            InputManager.Instance.OnCamInitiated -= CamSwitch;

    }



}
