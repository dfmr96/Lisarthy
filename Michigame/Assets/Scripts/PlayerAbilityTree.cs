using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilityTree : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerJump jump;
    [SerializeField] private PawTestScript paws;
    [SerializeField] private DashTestScript dash;
    [SerializeField] private HairballTestScript hairball;
    [SerializeField] private TailAttackTestScript tailAttack;

    private void Start()
    {

        if (PlayerPrefs.GetInt("Paws") == 1)
        {
            paws.enabled = true;
        } 
        
        if (PlayerPrefs.GetInt("Dash") == 1)
        {
            dash.enabled = true;
        } 
        if (PlayerPrefs.GetInt("Hairball") == 1)
        {
            hairball.enabled = true;
        }

        if (PlayerPrefs.GetInt("TailAttack") == 1)
        {
            tailAttack.enabled = true;
        }
    }
}
