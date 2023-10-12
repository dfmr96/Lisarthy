using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Snail : Enemy
{
    [SerializeField] private int direction;
    [SerializeField] private Transform point;
    private Rigidbody2D rb2d;
    private int layerMask;
    private int rotation = 0;
    private int fase = 0;
    private bool a = true;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(point.position, transform.TransformDirection(Vector2.right)*1.2f, Color.red);

    }

    private void FixedUpdate()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(point.position, transform.TransformDirection(Vector2.right), 1.2f, layerMask);
 

        if (hit.collider != null)
        {
            rb2d.velocity = transform.TransformDirection(Vector2.right);
        }
        else
        {
            rotation += 90;
            if (rotation >= 360)
            {
                rotation = 0;
            }
            rb2d.velocity = Vector2.zero;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,-rotation));
            
        }
    }
}
