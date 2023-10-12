using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HairballTestScript : MonoBehaviour
{
    public GameObject HairballPrefab;
    [SerializeField] private float speed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        HairballController hairball = Instantiate(HairballPrefab.GetComponent<HairballController>(), transform.position, Quaternion.identity);
        hairball.Direction = direction;
        hairball.Speed = speed;
    }
}
