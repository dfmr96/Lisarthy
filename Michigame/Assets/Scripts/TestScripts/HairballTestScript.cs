using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HairballTestScript : MonoBehaviour
{
    public GameObject HairballPrefab;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip shot;
    [SerializeField] private Transform hairballSpawn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.GetComponent<Animator>().SetTrigger("furBall");
            //Fire();
        }
    }

    private void Fire()
    {
        AudioManager.Instance.PlaySound(shot);
        Vector2 direction = PlayerMovement.isFacingRight ? new Vector2(1, 0) : new Vector2(-1, 0);
        HairballController hairball = Instantiate(HairballPrefab.GetComponent<HairballController>(), hairballSpawn.position, Quaternion.identity);
        hairball.Direction = direction;
        hairball.Speed = speed;
    }
}
