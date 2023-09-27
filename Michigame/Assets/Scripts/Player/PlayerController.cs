using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Animator animator;
    private float moveSpeed;
    private float jumpForce;
    [SerializeField] float jumpForceMultiplier = 2;
    private float maxJumpHeight =0;
    private float maxJumpTime;
    private float jumpTime;
    private bool canJump;
    private int jumpState;//0-on the ground / 1-Jumping / 2-Falling
    private float direction;
    private bool isJumping = false;
    private bool jumpButton;
    //Traduce los inputs


    // Start is called before the first frame update
    void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (jumpButton && jumpState ==0)
        {
            jumpState = 1;
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
        }
        else if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }

        if (direction != 0 && isJumping == false) 
        { 
            animator.SetBool("Walking", true); 
        }
        else if (direction == 0) { animator.SetBool("Walking", false); }

        // jump mechanics ------------------------------------------------

        //if (!canJump && jumpState == 0 && !jumpButton)
        //{
        //    jumpState = 2;
        //}
        if (canJump) { jumpState = 0; }

        //Si alcanza su altura maxima o si alcanza el tiempo maximo de salto o si esta callendo
        if (transform.position.y >= maxJumpHeight || jumpTime>=maxJumpTime || (!canJump && jumpState == 0 && !jumpButton))
        {
            jumpState = 2;
        }

        if (Input.GetButton("Jump"))
        {
            jumpButton = true;

            if (jumpState ==1)
            {
                jumpTime += Time.deltaTime;
                //jump higher if button remains pressed
                gameObject.GetComponent<PlayerPhysics>().SustainedJump(jumpForce, jumpForceMultiplier);
            }            
            
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpButton = false;
            jumpState = 2;

        }

        if (jumpState == 2) { jumpTime = 0; }

        isJumping = !gameObject.GetComponent<PlayerInfo>().CanJump();

    }

    private void UpdatePlayerInfo()
    {
        moveSpeed = gameObject.GetComponent<PlayerInfo>().moveSpeed;
        jumpForce = gameObject.GetComponent<PlayerInfo>().jumpForce;
        canJump = gameObject.GetComponent<PlayerInfo>().CanJump();
        maxJumpTime = playerInfo.maxJumpTime;
    }

    private void UpdatePlayerInput()
    {
        direction = Input.GetAxis("Horizontal");
       
    }

    private void PlayerJump()
    {
        Debug.Log("jump pressed");
        if (canJump)
        {
            Debug.Log("canJump");
            maxJumpHeight = gameObject.GetComponent<PlayerInfo>().CalculateMaxJumpHeight();
            gameObject.GetComponent<PlayerPhysics>().Jump(jumpForce);

        }
    }

}
