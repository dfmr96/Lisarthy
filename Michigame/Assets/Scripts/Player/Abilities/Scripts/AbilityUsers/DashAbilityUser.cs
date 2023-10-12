using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityUser : AbilityUser
{
    private float impulse;
    protected override void ExecuteAtAwake()
    {
        DashAbility dashAbility = ability as DashAbility;
        impulse = dashAbility.impulse;       
    }
    
    void Update()
    {

        DashAbility dashAbility = ability as DashAbility;
        impulse = dashAbility.impulse;
       
        UpdateCoolDown();
        if (currentCoolDown < 1.9)
        {
           var a = player.GetComponent<Rigidbody2D>().velocity;
           if (a.x is > 7 or < -7)
           {
               var b = a.normalized * 7;
               player.GetComponent<Rigidbody2D>().velocity = new Vector2(b.x,a.y);
           }
        }
        

        if (currentCoolDown == 0)
        {
            if (Input.GetKeyDown(ability.abilityKey) && Input.GetAxisRaw("Horizontal") != 0)
            {
                
                player.GetComponent<Rigidbody2D>().AddForce(transform.right * impulse * Mathf.Sign(Input.GetAxisRaw("Horizontal")), ForceMode2D.Impulse);
                currentCoolDown = coolDownTime;
            }
        }        
    }
}
