using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int hp;
    private float direction;
    public float moveSpeed;
    public float maxSpeed;
    [SerializeField]
    [Tooltip("increase the height of the jump the longer the jump button is pressed, until it reaches the maxJumpHeight.")]
    private bool incrementalJump; 
    public float jumpForce;
    public float maxJumpHeight;
    public float fallingSpeedMultiplier;
    public float terminalVelocity;
    private bool isTouchingGround;

    private void Update()
    {
        direction = Input.GetAxis("Horizontal");
    }

    public float GetDirection()
    {
        return direction;
    }

    //public bool CanJump()
    //{
        
    //}
}
