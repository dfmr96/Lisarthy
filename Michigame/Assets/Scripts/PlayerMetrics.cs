using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMetrics : ScriptableObject
{
    public float maxSpeed = 1f;
    public float maxAcceleration = 1f;
    public float maxDeccaleration = 1f;
    public float turnSpeed;

    public float jumpHeight = 5f;
    public float timeToJumpApex = 2f;
    public float upwardMultiplier = 1f;
    public float gravityMultiplier;
    
}