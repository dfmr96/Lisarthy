using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PawTestScript : MonoBehaviour
{
    [SerializeField] private Vector3 pawnBoxSize;
    [SerializeField] private float detectionDistance;
    [FormerlySerializedAs("climbableWall")] [SerializeField] private LayerMask climbableLayer;
    [SerializeField] private float maxFallingSpeedSliding = -2f;
    [SerializeField] private GameObject pawPrefab;
    [SerializeField] private GameObject paw;
    [SerializeField] private float pawDuration;
    [SerializeField] private float pawCooldown;
    [SerializeField] private bool onCooldown;
    [SerializeField] private Vector2 lastClimbDirection;
    [SerializeField] private Vector2 climbDir;
    [SerializeField] private bool wallJumpAvailable;
    [SerializeField] private AudioClip hit;

    public float MaxFallingSpeedSliding => maxFallingSpeedSliding;

    public bool WallJumpAvailable
    {
        get => wallJumpAvailable;
        set => wallJumpAvailable = value;
    }

    public Vector2 LastClimbDirection
    {
        get => lastClimbDirection;
        set => lastClimbDirection = value;
    }

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

    public void ResetClimbDir()
    {
        LastClimbDirection = Vector2.zero;
        climbDir = Vector2.zero;
        wallJumpAvailable = false;
    }

    public bool OnClimb()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        //climbDir = Vector2.zero;
        bool onClimb = Physics2D.BoxCast(transform.position, pawnBoxSize, 0,
            inputDir,
            detectionDistance, climbableLayer);
        if (onClimb)
        {
            climbDir = inputDir;
            //LastClimbDirection = climbDir;
            //lastClimbDirection = inputDir;
            Debug.Log($"Ultima direccion guardad = {LastClimbDirection}");
        }

        
        
        if (LastClimbDirection != climbDir)
        {
            WallJumpAvailable = true;
            LastClimbDirection = climbDir;
            Debug.Log($"Ultima direccion guardad = {LastClimbDirection} \n {inputDir}");
            Debug.Log($"Wall Jump disponible");
        }
        
        
        
        
        
        return onClimb;
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
        AudioManager.Instance.PlaySound(hit);
        yield return new WaitForSeconds(pawDuration);
        paw.transform.localPosition = Vector3.zero;
        paw.SetActive(false);
        yield return new WaitForSeconds(pawCooldown - pawDuration);
        onCooldown = false;
    }

    /*public void ChangeOffset(PlayerJump playerJump)
    {
        playerJump.RaycastOffset = 0.0125f;
    }*/
}
