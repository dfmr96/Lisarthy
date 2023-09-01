using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawAttack : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("Enemigo dañado");
            enemy.TakeDamage(damage);
        }
    }
}
