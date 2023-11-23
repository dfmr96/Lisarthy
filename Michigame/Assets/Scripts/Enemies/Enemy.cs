using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected AudioClip soundHit;
    [SerializeField] protected AudioClip soundDeath;

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

    public virtual void Die()
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
                //Destroy(gameObject);
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
        int temp = UnityEngine.Random.Range(0, 50);
        if (temp <= 30)
        {
            Instantiate(healthOrb, this.transform.position, transform.rotation);
        }
        else
        { 
            Instantiate(ammo, this.transform.position, transform.rotation);
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

