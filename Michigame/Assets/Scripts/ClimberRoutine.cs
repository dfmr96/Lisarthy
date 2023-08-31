using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberRoutine : MonoBehaviour
{
    [SerializeField] private int speed;
    //[SerializeField] private float switchTimer = 0;
    [SerializeField] private Vector3 dir;
    [SerializeField] private BoxCollider2D patrolBounds;
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        dir = transform.up;
    }

    private void Update()
    {
        transform.Translate(dir * (speed * Time.deltaTime));

        if (collider.bounds.max.y >= patrolBounds.bounds.max.y) dir = -transform.up;
        if (collider.bounds.min.y <= patrolBounds.bounds.min.y) dir = transform.up;
    }
}
