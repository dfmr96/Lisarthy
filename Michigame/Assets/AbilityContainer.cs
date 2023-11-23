using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AbilityType
{
    Paw,
    Hairball,
    Tail,
    Dash
}
public class AbilityContainer : MonoBehaviour
{
    [SerializeField] private AbilityType abilityToObtain;

    private void Take(GameObject player)
    {
        switch (abilityToObtain)
        {
            case AbilityType.Paw:
                if (player.TryGetComponent<PawTestScript>(out PawTestScript pawTestScript))
                {
                    pawTestScript.enabled = true;
                    PlayerPrefs.SetInt("Paws", 1);
                    Debug.Log("Garras tomadas");
                    if (player.TryGetComponent<PlayerJump>(out PlayerJump playerJump))
                    {
                        //pawTestScript.ChangeOffset(playerJump);
                        //Debug.Log("offset cambiado");
                    }
                }
                break;
            case AbilityType.Dash:
                if (player.TryGetComponent<DashTestScript>(out DashTestScript dashTestScript))
                {
                    dashTestScript.enabled = true;
                    PlayerPrefs.SetInt("Dash", 1);
                }
                break;
            case AbilityType.Hairball:
                if (player.TryGetComponent<HairballTestScript>(out HairballTestScript hairball))
                {
                    hairball.enabled = true;
                    PlayerPrefs.SetInt("Hairball", 1);
                }
                break;
            case AbilityType.Tail:
                if (player.TryGetComponent<TailAttackTestScript>(out TailAttackTestScript tail))
                {
                    tail.enabled = true;
                    PlayerPrefs.SetInt("TailAttack", 1);
                }
                break;
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Jugador tocado");
    //        Take(other.gameObject);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador tocado");
            Take(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
