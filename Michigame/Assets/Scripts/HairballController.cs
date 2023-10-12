using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairballController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask colliderMask;
    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject,2f);
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }

    private void Update()
    {
        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer & colliderMask) != 0)
        {
            Debug.Log("Destruido");
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log($"{enemy.health}");
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        
        Debug.Log($"Chocó + {1 << other.gameObject.layer} + {1 << colliderMask}");
    }
}
