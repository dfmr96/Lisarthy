using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HairballTestScript>(out HairballTestScript script))
        {
            script.AddAmmo();
            Destroy(this.gameObject);
        }
    }
}
