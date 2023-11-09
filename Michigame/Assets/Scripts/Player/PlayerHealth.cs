using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private AudioClip getdamage;
    [SerializeField] private AudioClip gethealth;
    // Start is called before the first frame update

    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlaySound(getdamage);
        health -= damage;
        Debug.Log(health);
    }
    public void TakeHealth(int heal)
    {
        AudioManager.Instance.PlaySound(gethealth);
        health += heal;
        Debug.Log(health);
    }
}
