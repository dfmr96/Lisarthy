using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public float speed;

    public int damage;
    
    
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (TryGetComponent<Animator>(out Animator animator))
        {
            gameObject.GetComponent<Animator>().SetTrigger("damaged");
        }        
        Die();
    }

    public void Die()
    {
        if (health <= 0)
        {
            if (TryGetComponent<Animator>(out Animator animator))
            {
                gameObject.GetComponent<Animator>().SetBool("isDead", true);
            }
            else
            {
                Destroy(gameObject);
            }               
            
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

