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
    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
            AudioManager.Instance.PlaySound(sound);
        }
    }

    [ContextMenu("Attack")]
    public void Attack() 
    {
        if (tail == null)
        {
            tail = Instantiate(tailPrefab, transform.position,
                Quaternion.identity, transform);
            anim = tail.GetComponent<Animator>();
            Debug.Log("Tail Collider creado");
        }
        else
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
            //StartCoroutine(SwingTail());
            
        }
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
}
