using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementForce;
    [SerializeField] private Animator animMove;
    
    [SerializeField] private GameObject camClose;
    [SerializeField] private GameObject camAway;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    private float jumpinput;
    private float hinput;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        sRenderer = GetComponent<SpriteRenderer>();
        camAway.SetActive(false);
        camClose.SetActive(true);
        
    }

    void Update()
    {
       
        hinput = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector2.right * (hinput * movementForce), ForceMode2D.Force);

        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CamSwitch();
        }
        MoveAnimations();
    }

    private void MoveAnimations()
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

    private void CamSwitch()
    {
        camClose.SetActive(!camClose.activeSelf);
        camAway.SetActive(!camAway.activeSelf);
    }



}
