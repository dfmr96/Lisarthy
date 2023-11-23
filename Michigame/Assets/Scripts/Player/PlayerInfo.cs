using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    private Animator animator;
    public int hp;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] int jumpState;//0-on the ground / 1-Jumping / 2-Falling
    [SerializeField] float jumpForce;
    [SerializeField] float jumpForceMiltiplier;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float maxJumpTime;
    [SerializeField] float fallSpeed;
    [SerializeField] float fallingSpeedMultiplier;
    [SerializeField] float terminalVelocity;
    [SerializeField] bool onFloor;
    public bool jumping;
    public bool dying = false;
    public bool dead = false;
    public bool climbing = false;
    public bool forceJump = false;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        UpdateJumpState();

        if (dead)
        {
            //gameManager.playerDead = true;
        }
        else if (dying)
        {
            DeathAnimation();
        }
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public float GetJumpForceMultiplier()
    {
        return jumpForceMiltiplier;
    }

    public float GetJumpHeight()
    {
        return maxJumpHeight;
    }

    public float GetMaxJumpTime()
    {
        return maxJumpTime;
    }

    public float CalculateMaxJumpHeight()
    {
        return transform.position.y + maxJumpHeight;
    }


    public bool CanJump()
    {
        return onFloor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        onFloor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onFloor = false;
    }

    public float GetFallSpeed()
    {
        return fallSpeed;
    }

    public float GetFallMultiplier()
    {
        return fallingSpeedMultiplier;
    }

    public float GetTerminalVelocity()
    {
        return terminalVelocity;
    }

    private void UpdateJumpState()
    {
        if (transform.position.y >= maxJumpHeight || playerController.GetJumpTime() >= maxJumpTime || (!onFloor && jumpState == 0 && !playerController.GetKey("jump")) || !playerController.GetKey("jump") && !onFloor)
        {
            jumping = false;
            jumpState = 2;
        }        
        else if (onFloor) 
        { 
            jumpState = 0;
            jumping = false;
        }
        else if (jumping) { jumpState = 1; }


    }

    public int GetJumpState()
    {
       return jumpState;
    }

    public void DeathAnimation()
    {
        animator.SetBool("Dead", true);
    }

    public void Dead()
    {
        dead = true;
    }

}
