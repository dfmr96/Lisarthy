using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    // Start is called before the first frame update

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
