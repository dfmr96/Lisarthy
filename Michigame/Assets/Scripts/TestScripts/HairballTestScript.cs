using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HairballTestScript : MonoBehaviour
{
    public GameObject HairballPrefab;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip shot;
    [SerializeField] private Transform hairballSpawn;
    [SerializeField] float fireRate;
    [SerializeField] LifeController Life;
    private float waitTime;
    [SerializeField] int maxAmmo = 5;
    public int ammo = 5;

    private void Update()
    {
        if (waitTime <= 0)
        {
            if (Input.GetButtonDown("Hairball"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("furBall");
                waitTime = fireRate;
            }
        }
        else if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        
    }

    private void Fire()
    {
        if (ammo > 0) 
        {
            AudioManager.Instance.PlaySound(shot);
            Vector2 direction = PlayerMovement.isFacingRight ? new Vector2(1, 0) : new Vector2(-1, 0);
            HairballController hairball = Instantiate(HairballPrefab.GetComponent<HairballController>(), hairballSpawn.position, Quaternion.identity);
            ammo--;
            hairball.Direction = direction;
            hairball.Speed = speed;
            Life.UpdateHair(ammo);
        }
        
    }

    public void AddAmmo()
    {
        if (ammo < maxAmmo)
        {
            ammo++;
            Life.UpdateHair(ammo);
        }        
    }

    private void OnEnable()
    {
        Life.ActivatePadre();
    }

    private void OnDisable()
    {
        Life.DesactivatePadre();
    }
}
