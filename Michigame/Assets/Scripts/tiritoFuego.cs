using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiritoFuego : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.right*3,ForceMode2D.Force);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            wall.TakeDamage(damage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    

}
