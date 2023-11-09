using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    // Start is called before the first frame update

    private void Update()
    {
        Test();
    }
    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log(health);
        }
        else if (health <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("dead");
        }
        
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void Test()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(11);
        }
    }
}
