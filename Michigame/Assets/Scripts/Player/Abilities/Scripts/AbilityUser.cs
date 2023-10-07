using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    //Variables Generales de las habilidades
    public Ability ability;
    private KeyCode abilityKey;
    private GameObject player;
    [SerializeField] GameObject hitBox;
    private BoxCollider2D trigger;
    private int id;
    private int level;
    private int souls;
    private float coolDownTime;
    private float currentCoolDown = 0f;
    private int damage;
    private float knockBack;
    private float stunDuration;

    //Variables Especificas de Garras
    public bool climbing = false;
    private float gravity;
    private float drag;
    public float climbSpeed;
    public float climbingDrag;
    public float exitImpulse;

    private void Awake()
    {
        player = gameObject;
        trigger = hitBox.GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetAbilityData();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoolDown();

        if (climbing)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                //player.transform.position += transform.up * climbSpeed * Time.deltaTime;
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, climbSpeed * Time.deltaTime), ForceMode2D.Impulse);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, climbSpeed * Time.deltaTime * -1), ForceMode2D.Impulse);
            }
        }
        else if (Input.GetKeyDown(abilityKey))// Si no esta trepando, activa el hit box de la habilidad para poder atacar.
        {            
            if (currentCoolDown == 0)
            {
                if (id == 0 || id == 2)
                {
                    hitBox.SetActive(true);// Tengan en cuenta que el hit box no se desactiva hasta que no golpee a un enemigo.
                }                
            }           

        }
        
    }

    private void GetAbilityData()
    {
        id = ability.id;
        abilityKey = ability.abilityKey;
        level = ability.level;
        coolDownTime = ability.coolDownTime;
        damage = ability.damage;
        knockBack = ability.knockBack;
        stunDuration = ability.stunDuration;
    }

    private void UpdateCoolDown()// Actualiza el cooldown de la habilidad
    {    
            if (currentCoolDown > 0)
            {
                currentCoolDown -= Time.deltaTime;
            }
            else if (currentCoolDown < 0)
            {
                currentCoolDown = 0;
            }        
    }

    private void IsCilmbing(bool state)
    {
        if (state == true)
        {
            gravity = player.GetComponent<Rigidbody2D>().gravityScale;// Guarda la gravedad actual del jugador
            drag = player.GetComponent<Rigidbody2D>().drag;// Guarda el linear drag actual del jugador
            player.GetComponent<PlayerMovement>().enabled = false;// Desactiva el movimiento del jugador
            player.GetComponent<PlayerJump>().enabled = false; // Desactiva el salto del jugador
            player.GetComponent<Rigidbody2D>().gravityScale = 0; // Pone la gravedad del jugador en cero para que pueda trepar sin caerse
            player.GetComponent<Rigidbody2D>().drag = climbingDrag; // modifica el linear drag del jugador para que pueda o no resbalar en la pared
        }
        else if(state == false)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = gravity;// Le devuelve la gravedad al jugador
            player.GetComponent<Rigidbody2D>().drag = drag; // Le devuelve al jugador el mismo linear drag que tenia antes de empezar a trepar
            player.GetComponent<PlayerMovement>().enabled = true;// Reactiva el movimiento del jugador
            player.GetComponent<PlayerJump>().enabled = true;// Reactiva el salto del jugador
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.GetComponent<IEnemy>() != null)//Verifica si el collider entro en contacto con un enemigo
        {
            collision.GetComponent<IEnemy>().TakeDamage(damage,stunDuration);//Aplica el daño y el stun del enemigo atravez de su intefaz
            currentCoolDown = coolDownTime;// Pone la habilidad en cooldown
            hitBox.SetActive(false);// Desactiva el hit box de la habilidad
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (id == 0 && collision.gameObject.CompareTag("Climbable"))// Verifica que la habilidad que tiene este AbilityUser son las Garra y si el jugador entro en contacto con un objeto que tiene el tag de "Trepable"
         {            
            climbing = true;// Permite ejecutar el codigo de movimiento vertical en el update de este script
            IsCilmbing(true); // Modifica varias cosas del player para que pueda trepar sin problemas
         }
         
         if (climbing && collision.gameObject.layer == 10 && Input.GetAxisRaw("Vertical") == -1)//
         {
            climbing = false;// Impide ejecutar el codigo de movimiento vertical en el update de este script
            IsCilmbing(false);// Modifica varias cosas del player para que vuelva a la normalidad
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Climbable") && Input.GetAxisRaw("Vertical") > 0) 
        {
            climbing = false;// Impide ejecutar el codigo de movimiento vertical en el update de este script
            IsCilmbing(false);// Modifica varias cosas del player para que vuelva a la normalidad
            player.GetComponent<Rigidbody2D>().AddForce(transform.right + (transform.up * exitImpulse * Time.deltaTime), ForceMode2D.Impulse);// Impulsa al jugador hacia arriva para que pueda "escapar" de la pared.
        }
        else if(collision.gameObject.CompareTag("Climbable"))
        {
            climbing = false; // Impide ejecutar el codigo de movimiento vertical en el update de este script
        }
    }
}
