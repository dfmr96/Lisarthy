using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Gato : MonoBehaviour , Idamagable
{
    public int health = 3;
    public int damage = 1;

    public int speed = 100;
    public int jumpStrenght = 100;

    
    public bool TocandoPiso = false;
    public bool SeguirSaltando = false;
    public bool Coyote = false;
    public bool pegando = false;
    public bool Tirafuego = false;
    public float timer = 0;
    private float timer2 = 1;
    private float timer4 = 1;
    private float CoyoteTime = 0;
    private Vector2 dir;
    private int maxSpeed = 15;
    private int deceleration = 50;
    public int jumpCount = 0;
    Quaternion a = default;
    [SerializeField] private Renderer garra;
    [SerializeField] private GameObject garras;
    [SerializeField] private GameObject tiroFuego;
    [SerializeField] private Transform garraPoint;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fueguito.OnDeath += Desloque1;
    }

    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer4 += Time.deltaTime;
        CoyoteTime += Time.deltaTime;
        /////////Movimiento
        var dirHor = Input.GetAxis("Horizontal");
        dir = new Vector2(dirHor, 0);
        Rotacion();
        //Coyotes();
        Atack();
        MaxSpeed();
    }
    private void FixedUpdate()
    {
        if (dir.x != 0)
        {
            rb.AddForce(dir * speed, ForceMode2D.Force);
        }
        else
        {
            Vector3 decelerationForce = new Vector3(-rb.velocity.x * deceleration, 0.0f, 0.0f);
            rb.AddForce(decelerationForce * Time.deltaTime);
        }
        Jump(1);
    }


// Deteccion de Piso---------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            jumpCount = 0;
            TocandoPiso = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            TocandoPiso = false;
        }
    }
//------------------------------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.layer == 8)
        {
            GetDamage();
        }
        if (collision.gameObject.layer == 9)
        {
            GetHealth();
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        CoyoteTime = 0;
        Coyote = true;
        //TocandoPiso = false;
    }

    private void Rotacion()
    {
        if (dir.x != 0)
        {
            if (dir.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 90));
            }
        }
    }

    private void Atack()
    {
        
        if (dir.x != 0)
        {
            if (dir.x > 0)
            {
                a = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                a = Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }
        if (Tirafuego && Input.GetKeyDown(KeyCode.Mouse1) && timer4 > 0.8)
        {
            var x =Instantiate(tiroFuego, garraPoint.position , a);
            timer4 = 0;
        }   
        if (Input.GetKeyDown(KeyCode.Mouse0) && timer2 > 0.8)
        {
            var x =Instantiate(garras, garraPoint.position ,transform.rotation ,transform.parent);
            x.transform.SetParent(transform);
            Destroy(x,(float)0.2);
            pegando = true;
            timer2 = 0;
        }
        else if (timer2 > 0.15)
        {
            pegando = false;
        }
    }

    private void MaxSpeed()
    {
        var velX = rb.velocity;
        velX.y = 0;
        rb.AddForce(velX * (float)-0.65, ForceMode2D.Force);
    }

    

    private void Jump(int jumps)
    {
        if (jumpCount < jumps)
        {
            if ((Input.GetKey(KeyCode.Space) && TocandoPiso))
            {
                ReiniciarVelocidadY();
                rb.AddForce(new Vector3(0, jumpStrenght * 10, 0), ForceMode2D.Force);
                TocandoPiso = false;
                timer = 0;
            }
            else if (Input.GetKey(KeyCode.Space) && timer < 0.2)
            {
                rb.AddForce(new Vector3(0, jumpStrenght, 0), ForceMode2D.Force);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCount++;
                //TocandoPiso = true;
            }
        }
    }

    private void ReiniciarVelocidadY()
    {
        var actualV = rb.velocity;
        actualV.y = 0;
        rb.velocity = actualV;
    }


    public void GetDamage()
    {
        health--;
    }

    public void GetHealth()
    {
        health++;
    }
    private void Desloque1()
    {
        Tirafuego = true;
    }
}
