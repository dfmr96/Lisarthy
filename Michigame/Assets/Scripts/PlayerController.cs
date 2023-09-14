using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    private float direction;
    //Traduce los inputs


    // Start is called before the first frame update
    void Start()
    {        
        
    }

    private void FixedUpdate()
    {

    }
    private void Update()
    {
        UpdatePlayerInfo();

//Horizontal movement----------------------------------------------------------------
        if (Input.GetAxis("Horizontal") != 0)
        {
            gameObject.GetComponent<PlayerPhysics>().MoveFoward(moveSpeed, direction);
        }

        // Adjust player horizontal rotation------------------------------------------------------
        if (direction > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }

    }

    private void UpdatePlayerInfo()
    {
        moveSpeed = gameObject.GetComponent<PlayerInfo>().moveSpeed;
        direction = gameObject.GetComponent<PlayerInfo>().GetDirection();
    }

}
