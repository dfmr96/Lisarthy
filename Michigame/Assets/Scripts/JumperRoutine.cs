using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class JumperRoutine : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private Vector3 dir = Vector3.right;
    private int jumpStep = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(dir * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpStep++;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            if (jumpStep >= 2)
            {
                dir *= -1;
                jumpStep = 0;
            }
        }
    }
}
