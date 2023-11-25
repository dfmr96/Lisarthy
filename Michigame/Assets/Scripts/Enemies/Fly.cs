using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Fly : Enemy
{
    [SerializeField] private int pointsQuantity;
    [SerializeField] private Transform[] points = new Transform[3];
    private int index = 0;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(health > 0)
        {
            var vec = points[index].position - transform.position;
            var norm = vec.normalized;
            rb2d.velocity = norm * speed;
            if (vec.magnitude < 0.1)
            {
                index++;
                if (index >= pointsQuantity)
                {
                    index = 0;
                }
            }
        }        
    }
    
}
