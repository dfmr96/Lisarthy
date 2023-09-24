using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementDebug : MonoBehaviour
{
    [SerializeField] TMP_Text stats;
    [SerializeField] private PlayerMovement player;

    void FixedUpdate()
    {
        stats.SetText(player.PlayerStats);
    }
}