using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawController : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log($"{enemy.health}");
            enemy.TakeDamage(damage);
        }
    }
}
