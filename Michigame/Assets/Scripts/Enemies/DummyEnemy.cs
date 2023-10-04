using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour, IEnemy
{
    public int health = 10;
    private bool stunned = false;
    private float stunTime = 0;
    private float stunTimer = 0;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else if (stunTime != 0)
        {
            if (stunned == false)
            {
                stunned = true;
            }
            stunTimer += Time.deltaTime;
            Debug.Log("Stunned for " + stunTimer.ToString());

        }
        else if (stunTime == 0 && stunned)
        {
            Debug.Log("Enemy no longer stunned");
            stunned = false;
        }

    }

    public void TakeDamage(int damage, float stun)
    {
        health -= damage;
        stunTime = stun;
    }

}
