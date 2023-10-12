using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float knockback;
    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void Push()
    {
     rigidbody.AddForce(new Vector2(1 * -transform.right.x, 1) * knockback, ForceMode2D.Impulse);
    }
}
