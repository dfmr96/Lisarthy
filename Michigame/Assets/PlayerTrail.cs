using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTrail : MonoBehaviour
{
    [SerializeField] TrailRenderer groundTrail;
    [SerializeField] TrailRenderer upwardTrail;
    [SerializeField] TrailRenderer downwardTrail;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
    }

    private void OnEnable()
    {
        playerJump.OnGravityValueChanged += ToggleTrails;
    }

    private void OnDisable()
    {
        playerJump.OnGravityValueChanged -= ToggleTrails;
    }

    private void ToggleTrails(bool onGround, bool isUpwards)
    {
        groundTrail.emitting = onGround;
        upwardTrail.emitting = isUpwards;
        downwardTrail.emitting = !isUpwards;
    }
}
