using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private float fireCounter = 0;
    [SerializeField] private GameObject bulletPrefab;

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        fireCounter += Time.deltaTime;

        if (fireCounter >= 1 / fireRate)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform);
            fireCounter = 0;
        }
    }
}
