using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    private bool active = false;
    private bool finished = false;
    public Sprite[] dialogs;
    private GameObject player;
    private SpriteRenderer npcSpeech;
    private int index = 0;

    private bool claws;
    private bool dash;
    private bool furBall;
    private bool tail;

    // Start is called before the first frame update
    void Start()
    {        
        npcSpeech = this.gameObject.GetComponent<SpriteRenderer>();
        npcSpeech.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            claws = player.GetComponent<PawTestScript>().enabled;
            dash = player.GetComponent<DashTestScript>().enabled;
            furBall = player.GetComponent<HairballTestScript>().enabled;
            tail = player.GetComponent<TailAttackTestScript>().enabled;
        }

        if (active && !finished)
        {
            GroundPlayer(false);
            if (dialogs[index] != null)
            {
                npcSpeech.sprite = dialogs[index];
            }
            else
            {
                Debug.Log("This scriptable object has no dialog sprite assigned");
            }


            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                npcSpeech.sprite = null;
                index++;
            }

            if (index >= dialogs.Length)
            {
                finished = true;
                active = false;
                GroundPlayer(true);
                EnableSkills();
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            active = true;
            player = collision.gameObject;
        }

    }
    
    private void GroundPlayer(bool active)
    {
        player.GetComponent<PlayerMovement>().enabled = active;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Animator>().SetBool("walking", active);
        player.GetComponent<PlayerJump>().enabled = active;
        player.GetComponent<PawTestScript>().enabled = false;
        player.GetComponent<DashTestScript>().enabled = false;
        player.GetComponent<HairballTestScript>().enabled = false;
        player.GetComponent<TailAttackTestScript>().enabled = false;
    }

    private void EnableSkills()
    {
        player.GetComponent<PawTestScript>().enabled = claws;
        player.GetComponent<DashTestScript>().enabled = dash;
        player.GetComponent<HairballTestScript>().enabled = furBall;
        player.GetComponent<TailAttackTestScript>().enabled = tail;
    }

}
