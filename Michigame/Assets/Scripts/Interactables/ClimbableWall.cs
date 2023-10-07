using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableWall : MonoBehaviour
{
    public bool climbing = false;
    private GameObject climbingEntity;
    public float climbSpeed;
    public float input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Vertical");
        if (climbingEntity != null)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                climbingEntity.transform.position += transform.up * climbSpeed * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                climbingEntity.transform.position += -transform.up * climbSpeed * Time.deltaTime;
            }
        }      

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && climbing)
    //    {            
    //        collision.GetComponent<PlayerMovement>().enabled = false;
    //        collision.GetComponent<PlayerJump>().enabled = false;
    //        climbingEntity = collision.gameObject;
    //        Debug.Log("Touching Wall");
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && climbing)
        {
            collision.GetComponent<PlayerMovement>().enabled = false;
            collision.GetComponent<PlayerJump>().enabled = false;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            climbingEntity = collision.gameObject;
            Debug.Log("Touching Wall");
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            climbing = false;
            collision.GetComponent<PlayerMovement>().enabled = true;
            collision.GetComponent<PlayerJump>().enabled = true;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            climbingEntity = null;
            Debug.Log("No longer on wall");
        }

        if (collision.name == "Ability Hit Box")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
