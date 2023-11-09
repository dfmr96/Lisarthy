using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frog : Enemy
{
    private Rigidbody2D rb2d;
    private float timer = 3;
    private float a;
    [SerializeField] private Transform player_transform;
    [SerializeField] private int jumpHeight = 10;
    [SerializeField] private bool OnGround;
    private Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetInteger("hp", health);
        rb2d = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player_transform = playerObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        gameObject.GetComponent<Animator>().SetBool("jumping", !OnGround);

        distance = player_transform.position - transform.position;
        if (distance.magnitude < 10 && timer > 3.5 && OnGround)
        {
            timer = 0;
            OnGround = false;            
            rb2d.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            var direction = distance.normalized;
            rb2d.AddForce(new Vector2(direction.x,0) * speed, ForceMode2D.Impulse);
                    
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            OnGround = true;            
            rb2d.velocity = Vector2.zero;
            
        }
    }
}
