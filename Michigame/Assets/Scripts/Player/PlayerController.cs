using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator animator;

    //Horizontal movement Variables
    private float moveSpeed;
    private float direction;

    //Jump Variables
    private float jumpForce;
    private float jumpForceMultiplier;
    private float maxJumpHeight = 0;
    private float maxJumpTime;
    private float jumpTime;
    private bool canJump;
    private int jumpState;//0-on the ground / 1-Jumping / 2-Falling
    private bool jumpButton;


    // Start is called before the first frame update
    void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
// Horizontal Movement--------------------------------------------------------------------------------------

        if (Input.GetAxis("Horizontal") != 0)
        {
            gameObject.GetComponent<PlayerPhysics>().MoveFoward(moveSpeed, direction);
        }

//------- Jumping -----------------------------------------------------------------------------
        
        if (jumpButton && jumpState ==0)
        {
            playerInfo.jumping = true;
            PlayerJump();            
        }

        //Long Jump
        if (jumpState == 1 && jumpButton)
        {
            //jump higher if button remains pressed
            jumpTime += Time.deltaTime;
            gameObject.GetComponent<PlayerPhysics>().SustainedJump(jumpForce, jumpForceMultiplier);
        }       
    }
    private void Update()
    {        
        UpdatePlayerInfo();
        UpdatePlayerInput();

//Horizontal movement----------------------------------------------------------------
        

// Adjust player horizontal rotation------------------------------------------------------
        if (direction > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }

        if (direction != 0 && jumpState == 0) 
        { 
            animator.SetBool("Walking", true); 
        }
        else if (direction == 0) { animator.SetBool("Walking", false); }

//------ jump button detection ------------------------------------------------

        if (Input.GetButton("Jump"))
        {
            jumpButton = true;           
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpButton = false;
        }

        if (jumpState == 2) { jumpTime = 0; }

    }

    private void UpdatePlayerInfo()
    {
        moveSpeed = playerInfo.GetMoveSpeed();
        jumpState = playerInfo.GetJumpState();
        jumpForce = playerInfo.GetJumpForce();
        jumpForceMultiplier = playerInfo.GetJumpForceMultiplier();
        canJump = playerInfo.CanJump();
        maxJumpTime = playerInfo.GetMaxJumpTime();
        jumpState = playerInfo.GetJumpState();
    }

    private void UpdatePlayerInput()
    {
        direction = Input.GetAxis("Horizontal");
       
    }

    private void PlayerJump()
    {
        if (canJump)
        {
            maxJumpHeight = gameObject.GetComponent<PlayerInfo>().CalculateMaxJumpHeight();
            gameObject.GetComponent<PlayerPhysics>().Jump(jumpForce);

        }
    }

//Returns if the specified key is being pressed or not-------------------------------------------------
    public bool GetKey(string actionKey)
    {
        if (actionKey == "jump")
        {
            return jumpButton;
        }

        else 
        {
            Debug.Log("The requested Key is invalid");
            return false; 
        }
    }

    public float GetJumpTime()
    {
        return jumpTime;
    }

}
