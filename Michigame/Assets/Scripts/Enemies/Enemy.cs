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
        gameObject.GetComponent<Animator>().SetTrigger("damaged");
        Die();
    }

    public void Die()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isDead", true);
            Destroy(gameObject);
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

