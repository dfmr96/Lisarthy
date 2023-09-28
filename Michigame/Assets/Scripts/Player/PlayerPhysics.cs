using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private PlayerInfo playerInfo;
    [SerializeField] float dragWhileJumping = 3;
    //[SerializeField] float antiGravityApex;
    private float drag;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();

    }
    private void Start()
    {
        drag = rigidbody.drag;
    }
    private void Update()
    {
        if (!playerInfo.CanJump())
        {
            rigidbody.drag = dragWhileJumping;
        }
        else { rigidbody.drag = drag; }

        if (playerInfo.GetJumpState() != 0 && transform.position.y < playerInfo.GetJumpHeight())
        {
            Fall(playerInfo.GetFallSpeed(), playerInfo.GetFallMultiplier());
        }
        //else if (playerInfo.GetJumpState() != 0 && transform.position.y >= playerInfo.GetJumpHeight())
        //{
        //    Fall(playerInfo.GetFallSpeed(), playerInfo.GetFallMultiplier()- antiGravityApex);
        //}

    }

    public void UpdateDrag(float value)
    {
        drag = value;
    }

    public void MoveFoward(float speed, float direction)
    {
        rigidbody.AddForce(new Vector2(speed * direction, rigidbody.velocity.y));
    }

    public void Jump(float force)
    {
        rigidbody.AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    public void SustainedJump(float force, float multiplier = 0.5f)
    {
        rigidbody.AddForce(transform.up * (force * multiplier), ForceMode2D.Force);
    }

    public void Fall(float force, float multiplier = 0.5f)
    {
        rigidbody.AddForce(-transform.up * (force * multiplier), ForceMode2D.Force);
    }
    
}
