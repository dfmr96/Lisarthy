using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    public Action<bool, bool> OnGravityValueChanged;

    [SerializeField] private PlayerMetrics _playerMetrics;

    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float timeToJumpApex = 2f;
    [SerializeField] private float upwardMultiplier = 1f;
    [SerializeField] private float downwardMultiplier = 2f;

    [SerializeField] private float jumpCutOffMultiplier = 5f;
    //private float maxAirJumps = 0;

    //[SerializeField] private bool variableJumpHeight;
    [SerializeField] private float gravityMultiplier;


    //private bool canJumpAgain = false;
    [SerializeField] private bool desiredJump;
    [SerializeField] private bool onGround;

    [SerializeField] private float groundLength = 0.95f;

    [FormerlySerializedAs("collider")] [SerializeField]
    private BoxCollider2D col;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float timeToApexDebug; //Debug USE ONLY
    [SerializeField] private float timeToGroundDebug; //Debug USE ONLY
    [SerializeField] float coyoteTimeCounter = 0;

    private bool currentlyJumping;
    private float defaultGravityScale;

    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferCounter = 0;
    [SerializeField] private float coyoteTime = 0.15f;
    //[SerializeField] private float jumpBuffer = 0.15f;

    private float jumpSpeed;
    private bool pressingJump;
    private Rigidbody2D rb;

    private Vector2 velocity;

    [SerializeField] private float raycastOffset = -0.1f;

    [FormerlySerializedAs("pawnTest")] public PawTestScript pawTest;
    [SerializeField] bool wallJumpBuffer = false;
    [SerializeField] private float wallJumpBufferTimer;

    public string JumpDebugInfo
    {
        get =>
            "\n \n Jump Debug Info " +
            " \n" +
            $" Vertical Velocity {rb.velocity.y} \n" +
            $" currentlyJumping? {currentlyJumping} \n" +
            $" Time to Apex: {timeToApexDebug} \n" +
            $" Time to Ground: {timeToGroundDebug} \n" +
            $" Jump Height = {jumpHeight}";
        set => throw new NotImplementedException();
    }

    public float JumpHeight
    {
        get => jumpHeight;
        set => jumpHeight = value;
    }

    public float TimeToJumpApex
    {
        get => timeToJumpApex;
        set => timeToJumpApex = value / 10;
    }

    public float UpwardMultiplier
    {
        get => upwardMultiplier;
        set => upwardMultiplier = value;
    }

    public float GravityMultiplier
    {
        get => gravityMultiplier;
        set => gravityMultiplier = value;
    }

    public bool OnGround
    {
        get
        {
            bool raycast = Physics2D.Raycast(new Vector3(col.bounds.max.x + raycastOffset, transform.position.y),
                               Vector2.down,
                               groundLength,
                               groundLayer)
                           || Physics2D.Raycast(new Vector3(col.bounds.min.x - raycastOffset, transform.position.y, 0),
                               Vector2.down,
                               groundLength, groundLayer);
            return raycast;
        }
    }

    public bool OnClimb
    {
        get
        {
            if (pawTest == null)
            {
                if (TryGetComponent<PawTestScript>(out PawTestScript pawnTestScript))
                {
                    if (pawnTestScript.isActiveAndEnabled)
                    {
                        pawTest = pawnTestScript;
                    }
                }

                return false;
            }
            else
            {
                return pawTest.OnClimb();
            }
        }
    }

    public float RaycastOffset
    {
        get => raycastOffset;
        set => raycastOffset = value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        defaultGravityScale = 1f;
    }


    private void Start()
    {
        //LoadData();
    }

    private void Update()
    {
        if (OnGround && !currentlyJumping) rb.velocity = new Vector2(rb.velocity.x, 0);

        if (currentlyJumping && rb.velocity.y > 0) timeToApexDebug += Time.deltaTime;
        if (currentlyJumping && rb.velocity.y < 0) timeToGroundDebug += Time.deltaTime;
        SetPhysics();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            desiredJump = true;
            pressingJump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space)) pressingJump = false;

        if (!currentlyJumping && !onGround)
        {
            coyoteTimeCounter += Time.deltaTime;
        }
        else
        {
            coyoteTimeCounter = 0;
        }

        if (jumpBuffer > 0)
        {
            if (desiredJump)
            {
                jumpBufferCounter += Time.deltaTime;

                if (jumpBufferCounter > jumpBuffer)
                {
                    desiredJump = false;
                    jumpBufferCounter = 0;
                }
            }
        }

        if (OnClimb && !wallJumpBuffer)
        {
            StartCoroutine(WallJumpBufferCounter());
        }
    }


    private void FixedUpdate()
    {
        velocity = rb.velocity;
        /*if (OnClimb) Apply Sliding
        {
            if (rb.velocity.y < pawTest.MaxFallingSpeedSliding) rb.velocity = new Vector2(rb.velocity.x, pawTest.MaxFallingSpeedSliding);
        }*/

        if (desiredJump)
        {
            //if (OnClimb) WallJump(); else Jump();
            Jump();
            
            rb.velocity = velocity;

            return;
        }
        CalculateGravity();
    }

    private void OnDrawGizmos()
    {
        if (OnGround)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(col.bounds.max.x + raycastOffset, transform.position.y, 0),
            new Vector3(col.bounds.max.x + raycastOffset, transform.position.y, 0) + Vector3.down * groundLength);
        Gizmos.DrawLine(new Vector3(col.bounds.min.x - raycastOffset, transform.position.y, 0),
            new Vector3(col.bounds.min.x - raycastOffset, transform.position.y, 0) + Vector3.down * groundLength);
    }

    private void CalculateGravity()
    {
        var isUpwards = false;


        if (OnGround)
        {
            currentlyJumping = false;
            gravityMultiplier = defaultGravityScale;
        }
        /*else if (OnClimb) Apply Sliding
        {
            Debug.Log("gravedad cambiada a deslizando");
            if (rb.velocity.y > 0) rb.velocity = Vector2.zero;
            gravityMultiplier = 0.01f;
        }*/
        else
        {
            switch (rb.velocity.y)
            {
                case > 0.5f:
                    if (pressingJump && currentlyJumping)
                    {
                        gravityMultiplier = upwardMultiplier;
                        isUpwards = true;
                    }
                    else
                    {
                        /*Vector2 cutOffVelocity = new Vector2(rb.velocity.x, 0);
                        rb.velocity = cutOffVelocity;*/
                        gravityMultiplier = jumpCutOffMultiplier;
                    }

                    break;
                case < -0.5f:
                    gravityMultiplier = downwardMultiplier;
                    break;
            }
        }

        //if (coyoteTimeCounter > 0 && coyoteTimeCounter < coyoteTime) gravityMultiplier = upwardMultiplier;
        OnGravityValueChanged?.Invoke(OnGround, isUpwards);
    }

    private void SetPhysics()
    {
        Vector2 newGravity = new(0, -2 * jumpHeight / (TimeToJumpApex * TimeToJumpApex));
        rb.gravityScale = newGravity.y / Physics2D.gravity.y * gravityMultiplier;
        //Debug.Log(gravityMultiplier);
    }

    private void Jump()
    {

        if (wallJumpBuffer)
        {
            
            WallJump();
            return;
        }
        if (OnGround || coyoteTimeCounter < coyoteTime && coyoteTimeCounter > 0.03f)
        {
            timeToApexDebug = 0;
            timeToGroundDebug = 0;
            coyoteTimeCounter = 0;
            jumpBufferCounter = 0;
            desiredJump = false;

            gravityMultiplier = upwardMultiplier;
            OnGravityValueChanged?.Invoke(OnGround, true);
            SetPhysics();

            jumpSpeed = MathF.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * jumpHeight);
            //Debug.Log(jumpSpeed + " velocidad de salto");
            velocity.y = jumpSpeed;
            currentlyJumping = true;
        }
    }

    private void WallJump()
    {
        
        gravityMultiplier = upwardMultiplier;
        SetPhysics();
        jumpSpeed = MathF.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * jumpHeight);
        Debug.Log(jumpSpeed + " velocidad de salto");
        velocity.y = jumpSpeed;
        //velocity.x = jumpSpeed * -Input.GetAxisRaw("Horizontal");
        currentlyJumping = true;
    }

    public void LoadData()
    {
        jumpHeight = _playerMetrics.jumpHeight;
        timeToJumpApex = _playerMetrics.timeToJumpApex;
        upwardMultiplier = _playerMetrics.upwardMultiplier;
        gravityMultiplier = _playerMetrics.gravityMultiplier;
    }

    private IEnumerator WallJumpBufferCounter()
    {
        Debug.Log("Wall Jump Buffer activado");
        wallJumpBuffer = true;
        yield return new WaitForSeconds(wallJumpBufferTimer);
        wallJumpBuffer = false;
        Debug.Log("Wall Jump Buffer desactivado");
    }
}