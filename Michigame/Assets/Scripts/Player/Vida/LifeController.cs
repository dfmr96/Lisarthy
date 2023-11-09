using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private GameObject petalo;
    [SerializeField] private GameObject padre;
    private List<GameObject> Petalos = new List<GameObject>();
    private PlayerHealth playerHealth;
    private int health;
    private int healthChanged;
    private int desplazamiento;
    
    void Start()
    {
        desplazamiento = 0;
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        health = playerHealth.health;
        for (int i = 0; i < health; i++)
        {
            var petaloIsta = Instantiate(petalo, transform.position + new Vector3(desplazamiento,0,0) , Quaternion.identity, padre.transform);
            desplazamiento += 85;
            Petalos.Add(petaloIsta);
        }
        
        
    }
    public void UpdateHealth(int playerHealth)
    {
        healthChanged = playerHealth;
        if (health < healthChanged)
        {
            var petaloIsta = Instantiate(petalo, transform.position + new Vector3(desplazamiento,0,0) , Quaternion.identity, padre.transform);
            desplazamiento += 85;
            Petalos.Add(petaloIsta);
            petaloIsta.transform.position = new Vector3(petaloIsta.transform.position.x, Petalos[0].transform.position.y, 0);
        }
        else
        {
            var index = Petalos.Count() -1;
            var petaloDestroy = Petalos[index];
            Petalos.Remove(petaloDestroy);
            Destroy(petaloDestroy);
            desplazamiento -= 85;
        }

        health = healthChanged;
    }
}
