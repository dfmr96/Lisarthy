using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Wasp : Enemy
{
    [SerializeField] private Transform player_transform;
    private Rigidbody2D rb2d;
    [SerializeField] private float timer = 3;
    private Vector2 distance;

    [SerializeField] private bool a = true;
    [SerializeField] private bool b = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Animator>().SetInteger("hp", health);
        rb2d = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player_transform = playerObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            timer += Time.deltaTime;
            distance = player_transform.position - transform.position;
        }
        
    }

    void FixedUpdate()
    {
        if (health > 0)
        {
            var dir = distance.normalized;

            if (distance.magnitude is > 5 and < 10 && a)
            {
                rb2d.velocity = dir * speed;
                b = true;
            }
            else if (b)
            {
                a = false;
                b = false;

            }
            if (!a && timer > 2)
            {
                a = true;
                timer = 0;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(dir * (speed * 1.8f), ForceMode2D.Impulse);
            }
            if (distance.normalized.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
    
}