using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementForce;
    [SerializeField] private Animator animMove;

    
    [SerializeField] private GameObject camClose;
    [SerializeField] private GameObject camAway;
    
    private SpriteRenderer sRenderer;
    private float jumpinput;
    private float hinput;
    private Vector2 movement;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        sRenderer = GetComponent<SpriteRenderer>();
        camAway.SetActive(false);
        camClose.SetActive(true);
        
    }

    void Update()
    {
       
        hinput = Input.GetAxisRaw("Horizontal");
        movement.x = hinput * movementForce * Time.deltaTime;
        transform.Translate(movement);
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
