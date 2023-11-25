using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInfo playerInfo;
    [SerializeField] float dragWhileJumping = 3;
    //[SerializeField] float antiGravityApex;
    private float drag;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();

    }
    private void Start()
    {
        drag = rb.drag;
    }
    private void FixedUpdate()
    {
        if (playerInfo.GetJumpState() != 0 && transform.position.y < playerInfo.GetJumpHeight())
        {
            Fall(playerInfo.GetFallSpeed(), playerInfo.GetFallMultiplier());
        }
    }

    private void Update()
    {
        if (!playerInfo.CanJump())
        {
            rb.drag = dragWhileJumping;
        }
        else { rb.drag = drag; }       

    }

    public void UpdateDrag(float value)
    {
        drag = value;
    }

    public void MoveFoward(float speed, float direction)
    {
        rb.AddForce(new Vector2(speed * Time.deltaTime * direction, rb.velocity.y));
    }

    public void Jump(float force)
    {
        rb.AddForce(transform.up * force * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void SustainedJump(float force, float multiplier = 0.5f)
    {
        rb.AddForce(transform.up * (force* Time.deltaTime * multiplier), ForceMode2D.Force);
    }

    public void Fall(float force, float multiplier = 0.5f)
    {
        rb.AddForce(-transform.up * (force * Time.deltaTime * multiplier), ForceMode2D.Force);
    }
    
}
