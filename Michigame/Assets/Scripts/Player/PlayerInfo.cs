using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private PlayerController playerController;
    public int hp;
    public float moveSpeed;
    public float maxSpeed;
    public int jumpState;
    public float jumpForce;
    public float maxJumpHeight;
    public float maxJumpTime;
    public float fallingSpeedMultiplier;
    public float terminalVelocity;
    public bool onFloor;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {

    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public float GetJumpHeight()
    {
        return maxJumpHeight;
    }

    public float GetJumpTime()
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
        return fallingSpeedMultiplier;
    }

    public float GetTerminalVelocity()
    {
        return terminalVelocity;
    }

    private void UpdateJumpState()
    {

    }

    public int GetJumpState()
    {
       return jumpState;
    }

}
