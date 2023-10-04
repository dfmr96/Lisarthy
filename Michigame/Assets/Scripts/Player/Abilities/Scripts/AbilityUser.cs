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
    private float coolDownTime;
    private float[] currentCoolDowns = new float[4];
    private int damage;
    private float knockBack;
    private float stunDuration;

    private void Awake()
    {
        player = gameObject.GetComponentInParent<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoolDowns();

        if (Input.GetKeyDown(abilityKey))
        {
            GetAbilityData();
            if (currentCoolDowns[id] == 0)
            {
                gameObject.SetActive(true);
            }
            

        }
        
    }

    private void GetAbilityData()
    {
        name = ability.name;
        id = ability.id;
        level = ability.level;
        coolDownTime = ability.coolDownTime;
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

    private void UpdateCoolDowns()
    {
        for (int i = 0; i < currentCoolDowns.Length; i++)
        {
            if (currentCoolDowns[i] > 0)
            {
                currentCoolDowns[i] -= Time.deltaTime;
            }
            else if (currentCoolDowns[i] < 0)
            {
                currentCoolDowns[i] = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IEnemy>() != null) 
        {
            collision.GetComponent<IEnemy>().TakeDamage(damage,stunDuration);
            currentCoolDowns[id] = coolDownTime;
        }
        else if (id == 1 && collision.CompareTag("Climbable"))
        {
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (id == 1 && collision.CompareTag("Climbable"))
        {
            

        }
    }
}
