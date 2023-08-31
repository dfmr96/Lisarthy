using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject credits;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gato"))
        {
            Debug.Log("Player chocó");
            credits.gameObject.SetActive(true);
        }
    }
}
