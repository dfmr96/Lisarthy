using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private float fireCounter = 0;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private bool canShoot = false;

    void Update()
    {
        if (canShoot) Shoot();
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
