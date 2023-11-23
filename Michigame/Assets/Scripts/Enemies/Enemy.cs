using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip soundHit;
    [SerializeField] private AudioClip soundDeath;

    [SerializeField] GameObject healthOrb;
    [SerializeField] GameObject ammo;

    public int health;

    public float speed;

    public int damage;
    
    
    public virtual void TakeDamage(int damage)
    {
        health -= damage;        
        if (TryGetComponent<Animator>(out Animator animator))
        {
            AudioManager.Instance.PlaySound(soundHit);
            //gameObject.GetComponent<Animator>().SetInteger("hp", health);
            gameObject.GetComponent<Animator>().SetTrigger("damaged");            
        }
        Die();
    }

    public void Die()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            AudioManager.Instance.PlaySound(soundDeath);
            //Destroy(gameObject);

            if (TryGetComponent<Animator>(out Animator animator))
            {
                DropItem();
                gameObject.GetComponent<Animator>().SetBool("isDead", true);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            AudioManager.Instance.PlaySound(soundHit);
        }
    }
    public void Kill()
    {        
        Destroy(gameObject);
    }

    private void DropItem()
    {
        float temp = UnityEngine.Random.Range(0, 1);
        if (temp > 0.5)
        {
            Debug.Log("drop health");
            Instantiate(healthOrb, this.transform.position, transform.rotation);
            Debug.Log(healthOrb);
            Debug.Log("health dropped");
        }
        else
        { 
            Debug.Log("drop ammo");
            Instantiate(ammo, this.transform.position, transform.rotation);
            Debug.Log(ammo);
            Debug.Log("ammo dropped");
        }
    }

    [ContextMenu("KillEnemy")]
    public void KillEnemy()
    {
        TakeDamage(health);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
        }
    }
}

