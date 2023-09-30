using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerMetrics _playerMetrics;
    private Rigidbody2D rb;
    private Vector2 velocity;

    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float timeToJumpApex = 2f;
    [SerializeField] private float upwardMultiplier = 1f;
    //[SerializeField] private float downwardMultiplier = 2f;
    //private float maxAirJumps = 0;

    [SerializeField] private bool variableJumpHeight;
    //private float jumpCutOff;
    private float speedLimit;
    //[SerializeField] private float coyoteTime = 0.15f;
    //[SerializeField] private float jumpBuffer = 0.15f;

    private float jumpSpeed;
    private float defaultGravityScale;
    [SerializeField] private float gravityMultiplier;


    //private bool canJumpAgain = false;
    [SerializeField] private bool desiredJump;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    private bool pressingJump;
    [SerializeField] private bool onGround;
    private bool currentlyJumping;

    [SerializeField] private float groundLength = 0.95f;
    [FormerlySerializedAs("collider")] [SerializeField] private BoxCollider2D col;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float timeToApexDebug = 0; //Debug USE ONLY
    public string JumpDebugInfo =>
        $"\n \n Jump Debug Info " +
        $" \n" +
        $" Vertical Velocity {rb.velocity.y} \n" +
        $" currentlyJumping? {currentlyJumping} \n" +
        $" Time to Apex: {timeToApexDebug} \n" +
        $" Jump Height = {jumpHeight}";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        defaultGravityScale = 1f;
    }

    public float JumpHeight => jumpHeight;

    public void SetJumpHeight(float jumpHeight)
    {
        this.jumpHeight = jumpHeight;
    }

    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        onGround =  Physics2D.Raycast(new Vector3(col.bounds.max.x, transform.position.y), Vector2.down, groundLength, groundLayer) 
                    || Physics2D.Raycast( new Vector3(col.bounds.min.x, transform.position.y,0), Vector2.down, groundLength, groundLayer);
        if (onGround && !currentlyJumping) rb.velocity = new Vector2(rb.velocity.x, 0);

        if (currentlyJumping && rb.velocity.y > 0) timeToApexDebug += Time.deltaTime;
        SetPhysics();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            desiredJump = true;
        }
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;

        if (desiredJump)
        {
            Jump();
            
            rb.velocity = velocity;
            
            return;
        }

        CalculateGravity();
    }

    private void CalculateGravity()
    {
        if (onGround)
        {
            gravityMultiplier = defaultGravityScale;
            currentlyJumping = false;
        }
        else
        {
            gravityMultiplier = upwardMultiplier;
        }
    }

    private void SetPhysics()
    {
       //
        Vector2 newGravity = new Vector2(0, -2 * jumpHeight / (timeToJumpApex * timeToJumpApex));
        rb.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravityMultiplier;
        Debug.Log(newGravity);
    }

    private void Jump()
    {
        if (onGround)
        {
            timeToApexDebug = 0;
            desiredJump = false;

            jumpSpeed = MathF.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * jumpHeight);

            velocity.y += jumpSpeed;
            currentlyJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (onGround)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(new Vector3(col.bounds.max.x,transform.position.y,0), new Vector3(col.bounds.max.x,transform.position.y ,0) + Vector3.down * groundLength);
        Gizmos.DrawLine(new Vector3(col.bounds.min.x,transform.position.y,0), new Vector3(col.bounds.min.x,transform.position.y ,0) + Vector3.down * groundLength);
    }

    public void LoadData()
    {
        jumpHeight = _playerMetrics.jumpHeight;
        timeToJumpApex = _playerMetrics.timeToJumpApex;
        upwardMultiplier = _playerMetrics.upwardMultiplier;
        gravityMultiplier = _playerMetrics.gravityMultiplier;
    }
}
