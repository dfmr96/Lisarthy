using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Boss : Enemy
{
    private Rigidbody2D rb2d;
    private Transform player_transform;
    [SerializeField] private bool OnGround;
    private Vector2 distance;
    [SerializeField] private Transform[] points = new Transform[4];
    [SerializeField] private Transform upperPoint;
    [SerializeField] private GameObject Enemies;
    private float timer;
    private int index;
    private bool derecha = true;
    private bool volando = true;
    private bool up = true;
    private bool impulso = true;
    private bool EnemiesBool = true;
    private bool Instantiate1 = true;
    private bool Instantiate2 = true;
    private bool setTimer = true;
    private bool Inicio = true;
    private bool IrArriba = true;
    private int CantidadDeImpulsos = 0;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        player_transform = playerObject.GetComponent<Transform>();
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Inicio)
        {
            rb2d.AddForce(new Vector2(0, (-speed * 5.2f)), ForceMode2D.Impulse);
            Inicio = false;
            timer = 0;
        }
        else if (IrArriba && timer > 2)
        {
            var vec = upperPoint.position - transform.position;
            var norm = vec.normalized; 
            rb2d.velocity = norm * speed;
            if (vec.magnitude < 1)
            {
                IrArriba = false;
                rb2d.velocity = Vector2.zero;
            }

        }
        else if (!Inicio && !IrArriba)
        {
            if (health <= 12 && Instantiate1)
            {
                if (setTimer)
                {
                    up = true;
                    timer = 0;
                    setTimer = false;
                }
                var vec = new Vector3(player_transform.position.x,upperPoint.position.y,0) - transform.position;
                if (up)
                {
                    var norm = vec.normalized;
                    rb2d.velocity = norm * speed;
                }
                if (vec.magnitude < 1)
                {
                    rb2d.velocity = Vector2.zero;
                    up = false;
                }
                if (EnemiesBool && timer > 3)
                {
                    Instantiate(Enemies, transform.position, Quaternion.identity, transform);
                    EnemiesBool = false;
                }

                if (timer > 13)
                {
                    EnemiesBool = true;
                    setTimer = true;
                    Instantiate1 = false;
                    volando = true;
                    up = true;
                    impulso = true;
                }
            }
            else if (health <= 6 && Instantiate2)
            {
                if (setTimer)
                {
                    up = true;
                    timer = 0;
                    setTimer = false;
                }
                var vec = new Vector3(player_transform.position.x,upperPoint.position.y,0) - transform.position;
                if (up)
                {
                    var norm = vec.normalized;
                    rb2d.velocity = norm * speed;
                }
                if (vec.magnitude < 1)
                {
                    rb2d.velocity = Vector2.zero;
                    up = false;
                }
                if (EnemiesBool && timer > 3)
                {
                    Instantiate(Enemies, transform.position, Quaternion.identity, transform);
                    EnemiesBool = false;
                }

                if (timer > 13)
                {
                    Instantiate2 = false;
                    volando = true;
                    up = true;
                    impulso = true;
                }
            }
            else if (volando)
            {
                var vec = points[index].position - transform.position;
                var norm = vec.normalized;
                rb2d.velocity = norm * speed;
                if (derecha && vec.magnitude < 1)
                {
                    index++;
                    if (index >= points.Length)
                    {
                        index = 3;
                        derecha = false;
                    }
                }
                else if (!derecha && vec.magnitude < 1)
                {
                    index--;
                    if (index < 0)
                    {
                        index = 0;
                        derecha = true;
                        volando = false;
                    }
                }
            }
            else if (health > 6)
            {
                if (up)
                {
                    var vec = new Vector3(player_transform.position.x,upperPoint.position.y,0) - transform.position;
                    var norm = vec.normalized;
                    rb2d.velocity = norm * speed;
                    if (vec.magnitude < 1)
                    {
                        rb2d.velocity = Vector2.zero;
                        timer = 0;
                        up = false;
                    }
                }
                else
                {
                    ImpulsoParaabajo(1, 5.5f);
                    if (CantidadDeImpulsos == 1)
                    {
                        volando = true;
                        CantidadDeImpulsos = 0;
                    }
                }
            }
            else if (health <= 6)
            {
                if (up)
                {
                    var vec = new Vector3(player_transform.position.x,upperPoint.position.y,0) - transform.position;
                    var norm = vec.normalized;
                    rb2d.velocity = norm * speed;
                    if (vec.magnitude < 1)
                    {
                        rb2d.velocity = Vector2.zero;
                        timer = 0;
                        up = false;
                    }
                }
                else
                {
                    ImpulsoParaabajo(3, 3.5f);
                    if (CantidadDeImpulsos == 3)
                    {
                        volando = true;
                        CantidadDeImpulsos = 0;
                    }
                }
            }
        }
        
    }

    private void ImpulsoParaabajo(int num, float time)
    {

            if (timer < 2)
            {
                transform.position = new Vector2(player_transform.position.x, transform.position.y);
            }
            else if (impulso)
            {
                rb2d.AddForce(new Vector2(0, (-speed * 4.5f)), ForceMode2D.Impulse);
                impulso = false;
                timer = 2;
            }
            else if (rb2d.velocity.magnitude < 1 && timer < time && timer > 2.5)
            {
                rb2d.velocity = Vector2.zero;
            }
            else if (timer > time)
            {
                CantidadDeImpulsos++;
                up = true;
                impulso = true;
            }
        
    }

    private void OnDestroy()
    {
        
    }

    public override void Die()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            AudioManager.Instance.PlaySound(soundDeath);
            //Destroy(gameObject);

            if (TryGetComponent<Animator>(out Animator animator))
            {
                gameObject.GetComponent<Animator>().SetBool("isDead", true);
                
                //Destroy(gameObject);
            }
            else
            {
                if (GameManager.instance != null) GameManager.instance.GameOver(GameState.Victory);
                Time.timeScale = 0;
                PlayerPrefs.DeleteAll();
                Destroy(gameObject);
            }

        }
        else
        {
            AudioManager.Instance.PlaySound(soundHit);
        }
    }
}
