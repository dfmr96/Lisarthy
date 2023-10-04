using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{
    public Ability ability;
    public KeyCode abilityKey;
    private GameObject player;
    private int id;
    private int level;
    private int souls;
    private float coolDown;
    private float damage;
    private float knockBack;
    private float stunDuration;

    private void Awake()
    {
        player = gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(abilityKey))
        {
            GetAbilityData();


        }
        
    }

    private void GetAbilityData()
    {
        name = ability.name;
        id = ability.id;
        level = ability.level;
        coolDown = ability.coolDown;
        damage = ability.damage;
        knockBack = ability.knockBack;
        stunDuration = ability.stunDuration;
    }

    private void Attack(int abilityID)
    {
        if (abilityID == 1 || abilityID == 3)
        {

        }
        else if (abilityID == 2)
        {

        }
        else if (abilityID == 4)
        {

        }

    }

    private void ExplorationUse(int abilityID)
    {

    }
}
