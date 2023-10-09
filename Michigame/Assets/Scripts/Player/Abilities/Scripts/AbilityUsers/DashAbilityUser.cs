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

    // Update is called once per frame
    void Update()
    {
        // Para propositos de prueba-------------------------------------------
        DashAbility dashAbility = ability as DashAbility;
        impulse = dashAbility.impulse;
      //--------------------------------------------------------------------------------------------------
        
        UpdateCoolDown();

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
