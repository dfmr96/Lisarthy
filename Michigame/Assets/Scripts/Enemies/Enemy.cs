using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip soundHit;
    [SerializeField] private AudioClip soundDeath;
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

            AudioManager.Instance.PlaySound(soundDeath);
            Destroy(gameObject);

            if (TryGetComponent<Animator>(out Animator animator))
            {
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

