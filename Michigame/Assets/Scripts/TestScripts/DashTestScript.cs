using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashTestScript : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private float impulseForce;
    [SerializeField] private bool onCooldown;
    [SerializeField] private float dashDuration;

    [SerializeField] private TrailRenderer dashTrail;

    [SerializeField] private bool onDash;


    public bool OnDash() => onDash;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Dash();
        }
    }

    private void Dash()
    {
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        if (!onCooldown)
        {
            Vector2 impulseVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (impulseVector == Vector2.zero) yield break;
            
            dashTrail.emitting = true;
            Vector2 velocity = rb.velocity;
            rb.AddForce(impulseVector * impulseForce, ForceMode2D.Impulse);
            onCooldown = true;
            
            yield return new WaitForSeconds(dashDuration);
            dashTrail.emitting = false;
            rb.velocity = velocity;
            
            yield return new WaitForSeconds(cooldown - dashDuration);
            onCooldown = false;
        }
    }
}