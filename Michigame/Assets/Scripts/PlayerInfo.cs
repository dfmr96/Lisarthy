using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int hp;
    public Vector2 currentPos;
    public float moveSpeed;
    public float maxSpeed;
    public Vector2 direction;

    private void Update()
    {
        currentPos = this.transform.position;
    }
}
