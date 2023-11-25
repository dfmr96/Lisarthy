using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("healOnly"))
            {
                playerHealth.TakeHealth(1);
                Destroy(gameObject);
            }
            else
            {
                playerHealth.IncreaseHealth();
                playerHealth.TakeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
