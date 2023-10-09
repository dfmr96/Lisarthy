using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailPunchAbilityUser : AbilityUser
{
    public bool active = false;
    protected override void ExecuteAtAwake() 
    { 

    }
    void Update()
    {
        coolDownTime = ability.coolDownTime;

        UpdateCoolDown();

        if (currentCoolDown == 0 && Input.GetKeyDown(abilityKey))
        {
            if (TryGetComponent<ClawsAbilityUser>(out ClawsAbilityUser component) && component.active == false)
            {
                hitBox.SetActive(true);// Tengan en cuenta que el hit box no se desactiva hasta que no golpee a un enemigo o una superficie rompible.
                active = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IEnemy>() != null && active)//Verifica si el collider entro en contacto con un enemigo
        {
            collision.GetComponent<IEnemy>().TakeDamage(damage, stunDuration);//Aplica el daño y el stun del enemigo atravez de su intefaz
            currentCoolDown = coolDownTime;// Pone la habilidad en cooldown
            hitBox.SetActive(false);// Desactiva el hit box de la habilidad
            active = false;
        }
        else if (collision.GetComponent<ITailinteractionable>() != null && active)
        {
            collision.GetComponent<ITailinteractionable>().TailPunchAction();
            currentCoolDown = coolDownTime;
            hitBox.SetActive(false);
            active = false;
        }
    }
}
