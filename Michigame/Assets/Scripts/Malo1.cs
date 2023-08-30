using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Malo1 : MonoBehaviour , Idamagable
{
    private Transform player;
    [SerializeField] private GameObject fueguit;
    
    private Rigidbody2D rb;
    public bool pego;
    //public float timer = 1;

    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        //Gato.OnHit += GetDamage1;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Gato").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        if ((player.position - transform.position).magnitude < 10)
        {
            rb.AddForce(player.position - transform.position, ForceMode2D.Force);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 10)
        {
            if (other.gameObject.CompareTag("Fueguit"))
            {
                Destroy(other.gameObject);
            }
            rb.AddForce((player.position-transform.position)*-5, ForceMode2D.Impulse);
            GetDamage1(1);
            Die();
        }

    }

    private void Die()
    {
        if (health <= 0)
        {
            Instantiate(fueguit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    public void GetDamage1(int n)
    {
        health -= n;
    }

    public void GetDamage()
    {
        throw new NotImplementedException();
    }

    public void GetHealth()
    {
        health++;
    }
}
