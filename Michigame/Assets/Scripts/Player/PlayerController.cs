using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float moveSpeed;
    private float jumpForce;
    [SerializeField] float jumpForceMultiplier = 2;
    private float maxJumpHeight;
    private bool canJump;
    private float direction;
    private bool isJumping = false;
    //Traduce los inputs


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            PlayerJump();
            
        }
    }
    private void Update()
    {        
        UpdatePlayerInfo();
        UpdatePlayerInput();

//Horizontal movement----------------------------------------------------------------
        if (Input.GetAxis("Horizontal") != 0)
        {
            gameObject.GetComponent<PlayerPhysics>().MoveFoward(moveSpeed, direction);
        }

// Adjust player horizontal rotation------------------------------------------------------
        if (direction > 0)
        {
            transform.eulerAngles = Vector3.zero;
            animator.SetTrigger("Walking");
        }
        else if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
            animator.SetTrigger("Walking");
        }        

//Check if jump button is pressed------------------------------------------------

        if (Input.GetButton("Jump"))
        {            
            isJumping = true;

            if (canJump)
            {
                gameObject.GetComponent<PlayerPhysics>().SustainedJump(jumpForce, jumpForceMultiplier);
            }            
            
        }
        if (isJumping)
        {
            if (transform.position.y >= maxJumpHeight)
            {
                canJump = false;
            }
        }
        
        if (Input.GetButtonUp("Jump"))
        {
            //jumpForce = localJumpForce;
            isJumping = false;

        }

    }

    private void UpdatePlayerInfo()
    {
        moveSpeed = gameObject.GetComponent<PlayerInfo>().moveSpeed;
        jumpForce = gameObject.GetComponent<PlayerInfo>().jumpForce;
    }

    private void UpdatePlayerInput()
    {
        direction = Input.GetAxis("Horizontal");
       
    }

    private void PlayerJump()
    {
        Debug.Log("jump pressed");
        if (gameObject.GetComponent<PlayerInfo>().CanJump())
        {
            Debug.Log("canJump");
            canJump = true;
            maxJumpHeight = gameObject.GetComponent<PlayerInfo>().CalculateMaxJumpHeight();
            gameObject.GetComponent<PlayerPhysics>().Jump(jumpForce);

        }
    }

}
