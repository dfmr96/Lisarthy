using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurBall : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeSpan;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeSpan > 0)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            lifeSpan -= Time.deltaTime;
        }
        else if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("furBall Destructible"))
        {  
            Destroy(this.gameObject); 
        }
    }
}
