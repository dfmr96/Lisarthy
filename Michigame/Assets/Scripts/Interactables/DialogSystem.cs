using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    private bool active = false;
    private bool finished = false;
    public Dialog[] dialogs;
    private GameObject player;
    private SpriteRenderer playerSpeech;
    private SpriteRenderer npcSpeech;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeech = GameObject.Find("PlayerDialog").GetComponent<SpriteRenderer>();
        npcSpeech = this.gameObject.GetComponent<SpriteRenderer>();
        npcSpeech.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {        
        if (active && !finished)
        {
            GroundPlayer(false);
            if (dialogs[index].dialog != null)
            {
                if (dialogs[index].isPlayerDialog)
                {
                    playerSpeech.sprite = dialogs[index].dialog;
                }
                else
                {
                    npcSpeech.sprite = dialogs[index].dialog;
                }
            }
            else
            {
                Debug.Log("This scriptable object has no dialog sprite assigned");
            }


            if (Input.GetMouseButtonDown(0))
            {
                playerSpeech.sprite = null;
                npcSpeech.sprite = null;
                index++;
            }

            if (index >= dialogs.Length)
            {
                finished = true;
                active = false;
                GroundPlayer(true);
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
        player.GetComponent<PawTestScript>().enabled = active;
        player.GetComponent<DashTestScript>().enabled = active;
        player.GetComponent<HairballTestScript>().enabled = active;
        player.GetComponent<TailAttackTestScript>().enabled = active;
    }

}
