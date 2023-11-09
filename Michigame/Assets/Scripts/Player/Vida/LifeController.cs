using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private GameObject petalo;
    
    void Start()
    {
        
        GameObject playerObject = GameObject.FindWithTag("Player");
        //player_transform = playerObject.GetComponent<Transform>();
    }
}
