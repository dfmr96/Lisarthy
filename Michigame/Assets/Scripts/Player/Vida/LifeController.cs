using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private GameObject petalo;
    [SerializeField] private GameObject hairball;
    [SerializeField] private GameObject padre;
    [SerializeField] private GameObject padre2;
    private List<GameObject> Petalos = new List<GameObject>();
    private List<GameObject> Hairballs = new List<GameObject>();
    private PlayerHealth playerHealth;
    private HairballTestScript playerHair;
    private int health;
    private int ammo;
    private int ammoChanged;
    private int healthChanged;
    private int desplazamiento;
    
    void Start()
    {
        desplazamiento = 0;
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        playerHair = playerObject.GetComponent<HairballTestScript>();
        health = playerHealth.health;
        ammo = playerHair.ammo;
        for (int i = 0; i < health; i++)
        {
            var petaloIsta = Instantiate(petalo, transform.position + new Vector3(desplazamiento,0,0) , Quaternion.identity, padre.transform);
            desplazamiento += 85;
            Petalos.Add(petaloIsta);
        }
        for (int i = 0; i < health; i++)
        {
            var hairinsta = Instantiate(hairball, transform.position + new Vector3(desplazamiento,0,0) , Quaternion.identity, padre2.transform);
            desplazamiento += 85;
            Hairballs.Add(hairinsta);
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
    public void UpdateHair(int Ammo)
    {
        ammoChanged = Ammo;
        if (ammo < ammoChanged)
        {
            var hairinsta = Instantiate(hairball, transform.position + new Vector3(desplazamiento,0,0) , Quaternion.identity, padre2.transform);
            desplazamiento += 85;
            Hairballs.Add(hairinsta);
            hairinsta.transform.position = new Vector3(hairinsta.transform.position.x, Hairballs[0].transform.position.y, 0);
        }
        else
        {
            var index = Hairballs.Count() -1;
            var petaloDestroy = Hairballs[index];
            Hairballs.Remove(petaloDestroy);
            Destroy(petaloDestroy);
            desplazamiento -= 85;
        }

        ammo = ammoChanged;
    }

    public void Restart()
    {
        desplazamiento = 0;
        health = playerHealth.health;
        Petalos.Clear();
        for (int i = 0; i < health; i++)
        {
            var petaloIsta = Instantiate(petalo, transform.position + new Vector3(desplazamiento,0,0) , Quaternion.identity, padre.transform);
            desplazamiento += 85;
            Petalos.Add(petaloIsta);
        }
    }

    public void ActivatePadre()
    {
        padre2.SetActive(true);
    }
    public void DesactivatePadre()
    {
        padre2.SetActive(false);
    }
}
