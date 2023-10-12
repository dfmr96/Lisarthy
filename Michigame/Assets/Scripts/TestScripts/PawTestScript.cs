using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PawTestScript : MonoBehaviour
{
    [SerializeField] private Vector3 pawnBoxSize;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask climbableWall;
    [SerializeField] private float maxFallingSpeedSliding = -2f;
    [SerializeField] private GameObject pawPrefab;
    [SerializeField] private GameObject paw;
    [SerializeField] private float pawDuration;
    [SerializeField] private float pawCooldown;
    [SerializeField] private bool onCooldown;

    public float MaxFallingSpeedSliding => maxFallingSpeedSliding;

    /*private void FixedUpdate()
    {
        if (OnClimb())
        {
            Debug.Log("Puedes trepar");
        }
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Atacó con garra");
            Attack();
        }
    }

    public bool OnClimb()
    {
        return Physics2D.BoxCast(transform.position, pawnBoxSize, 0, new Vector2(Input.GetAxisRaw("Horizontal"), 0),
            detectionDistance, climbableWall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;


        Vector3 cubeCenter = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") * detectionDistance,
            transform.position.y, transform.position.z);
        Gizmos.DrawCube(cubeCenter, pawnBoxSize);
    }

    public void Attack()
    {
        if (paw == null)
        {
            paw = Instantiate(pawPrefab, transform.position, Quaternion.identity, transform);
        }
        else if (!onCooldown)
        {
            StartCoroutine(Swing());
        }
    }

    public IEnumerator Swing()
    {
        float inputRaw = Input.GetAxisRaw("Horizontal") == 0 ? 1 : Input.GetAxisRaw("Horizontal");
        
        paw.transform.localPosition = new Vector3((inputRaw), 0, 0);
        
        
        onCooldown = true;
        paw.SetActive(true);
        yield return new WaitForSeconds(pawDuration);
        paw.transform.localPosition = Vector3.zero;
        paw.SetActive(false);
        yield return new WaitForSeconds(pawCooldown - pawDuration);
        onCooldown = false;
    }

    public void ChangeOffset(PlayerJump playerJump)
    {
        playerJump.RaycastOffset = 0.0125f;
    }
}
