using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Enemy
{
    private Rigidbody2D rb2d;
    [SerializeField] private Transform player_transform;
    private Vector2 distance;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player_transform = playerObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = player_transform.position - transform.position;
        if (distance.magnitude < 8)
        {
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            transform.position = new Vector2(transform.position.x,(player_transform.position.y +(transform.localScale.y - 1 )/2));
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }
    }
}
