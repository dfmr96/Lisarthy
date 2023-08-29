using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class fueguito : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Transform player;
    private bool a = false;
    public static Action OnDeath;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Gato").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var vec = player.position - transform.position;
        if (vec.magnitude < 6 || a)
        {
            if (!a)
            {
                rb.AddForce(-vec.normalized*8+ Vector3.up*2,ForceMode2D.Impulse);
            }
            a = true;
            rb.AddForce(vec.normalized*6,ForceMode2D.Force);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject);
            OnDeath?.Invoke();
        }
    }
}
