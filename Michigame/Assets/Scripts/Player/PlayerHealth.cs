using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    [SerializeField] private LifeController life;
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
            life.UpdateHealth(health);
        }
        else if (health <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("dead");
        }
        
    }

    public void TakeHealth(int heal)
    {
        health += heal;
        life.UpdateHealth(health);
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
