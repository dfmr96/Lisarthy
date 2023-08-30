using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(transform.right * (speed * Time.deltaTime));
    }
}
