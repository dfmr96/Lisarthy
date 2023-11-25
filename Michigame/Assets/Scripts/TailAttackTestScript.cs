using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TailAttackTestScript : MonoBehaviour
{
    
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private Vector2 offset;
    [SerializeField] private GameObject tail;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip sound;

    private void Start()
    {
        anim = tail.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") < 0 && Input.GetButtonDown("Attack"))
        {
            Attack();
            
            
            //Attack();

        }
    }

    [ContextMenu("Attack")]
    public void Attack() 
    {
            anim.SetTrigger("Attack");
            gameObject.GetComponent<Animator>().SetTrigger("tailPunch");
            AudioManager.Instance.PlaySound(sound);
    }

    public IEnumerator SwingTail()
    {
        /*GameObject tail = tailCollider.gameObject;
        
        Debug.Log("SwingTail");

        tail.transform.position = new Vector2(transform.parent.position.x + offset.x + Input.GetAxisRaw("Horizontal"),
            transform.parent.position.y + offset.y);
        */
        yield return null;
    }

    public void Attack2()
    {
        Attack();
    }
}
