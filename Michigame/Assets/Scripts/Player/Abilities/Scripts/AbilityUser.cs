using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityUser : MonoBehaviour
{
    //Variables Generales de las habilidades
    public Ability ability;
    protected KeyCode abilityKey;
    protected GameObject player;
    public GameObject hitBox;
    protected int id;
    protected int level;
    protected int souls;
    protected float coolDownTime;
    protected float currentCoolDown = 0f;
    protected int damage;
    protected float knockBack;
    protected float stunDuration;

    private void Awake()
    {
        ExecuteAtAwake();
        player = gameObject;
        GetAbilityData();
    }

    protected void GetAbilityData()
    {
        id = ability.id;
        abilityKey = ability.abilityKey;
        level = ability.level;
        coolDownTime = ability.coolDownTime;
        damage = ability.damage;
        knockBack = ability.knockBack;
        stunDuration = ability.stunDuration;
    }


    protected void UpdateCoolDown()// Actualiza el cooldown de la habilidad
    {    
            if (currentCoolDown > 0)
            {
                currentCoolDown -= Time.deltaTime;
            }
            else if (currentCoolDown < 0)
            {
                currentCoolDown = 0;
            }        
    }

    protected abstract void ExecuteAtAwake();
}
