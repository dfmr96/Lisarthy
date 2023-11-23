using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    [SerializeField] private GameObject VirtualCamera;
    [SerializeField] private GameObject BossCameraCinematic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            
        }
    }
}
