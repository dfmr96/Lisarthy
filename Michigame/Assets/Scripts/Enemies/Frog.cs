using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpIntervalSeconds;
    private float jumpTimer;
    [SerializeField] int maxJumps;
    private int jumps;
    private bool onFloor;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (JumpTimer() && jumps < maxJumps && onFloor)
        {
            Movement();
        }
        if (jumps >= maxJumps)
        {
            Rotate();
        }
    }

    private void Movement()
    {
        rigidbody.AddForce(new Vector2(transform.right.x * moveSpeed, transform.up.y * jumpForce), ForceMode2D.Impulse);
        jumps++;
        jumpTimer = 0;
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        jumps = 0;
    }

    private bool JumpTimer()
    {
        if (onFloor)
        {
            if (jumpTimer >= jumpIntervalSeconds)
            {
                return true;
            }            
        }
        jumpTimer += Time.deltaTime;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("floor"))
        {
            onFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("floor"))
        {
            onFloor = false;
        }
    }
}
