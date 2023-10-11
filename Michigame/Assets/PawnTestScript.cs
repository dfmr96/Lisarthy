using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PawnTestScript : MonoBehaviour
{
    [SerializeField] private Vector3 pawnBoxSize;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask climbableWall;

    /*private void FixedUpdate()
    {
        if (OnClimb())
        {
            Debug.Log("Puedes trepar");
        }
    }*/

    public bool OnClimb()
    {
        return Physics2D.BoxCast(transform.position, pawnBoxSize, 0, new Vector2(Input.GetAxisRaw("Horizontal"),0), detectionDistance, climbableWall);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        
        Vector3 cubeCenter = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") * detectionDistance,
            transform.position.y, transform.position.z);
        Gizmos.DrawCube( cubeCenter, pawnBoxSize);
    }
}
