using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantObstacle : MonoBehaviour
{
    public void Break()
    {        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("furBall"))
        {
            Destroy(collision.gameObject);
            Break();
        }
    }
}
