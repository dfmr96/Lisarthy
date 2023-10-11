using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb2d;
    private Transform player_transform;

    private Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = player_transform.position - transform.position;
        if (distance.magnitude > 10)
        {
            var direction = distance.normalized;
            rb2d.AddForce(direction * 10, ForceMode2D.Force);
        }
    }
}
