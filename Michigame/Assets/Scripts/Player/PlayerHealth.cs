using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    // Start is called before the first frame update

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log(health);
        }
        else if (health <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("dead", true);
        }
        
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
