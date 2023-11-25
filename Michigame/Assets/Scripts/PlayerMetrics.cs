using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PlayerMetrics : ScriptableObject
{
    public float maxSpeed = 1f;
    public float maxFallingSpeed = -15f;
    public float maxAcceleration = 1f;
    public float maxDeceleration = 1f;
    public float maxAirAcceleration = 1f;
    public float maxAirDeceleration = 2f;
    public float turnSpeed;
    public float maxAirTurnSpeed;

    public float jumpHeight = 5f;
    public float timeToJumpApex = 2f;
    public float upwardMultiplier = 1f;
    public float downwardMultiplier = 1f;
    public float jumpCutOffMultipler = 2f;
    public float gravityMultiplier;
    
}