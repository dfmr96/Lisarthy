using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private AudioClip getdamage;
    [SerializeField] private AudioClip gethealth;

    [SerializeField] private LifeController life;
    // Start is called before the first frame update

    private void Update()
    {
        Test();
    }
    public void TakeDamage(int damage)
    {

        AudioManager.Instance.PlaySound(getdamage);
        health -= damage;
        Debug.Log(health);
        
        if (health > 0)
        {
            
            health -= damage;
            life.UpdateHealth(health);
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
    public void TakeHealth(int heal)
    {
        AudioManager.Instance.PlaySound(gethealth);
        health += heal;
        life.UpdateHealth(health);
        Debug.Log(health);
    }
}
