using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    public GameObject objectToActive;

    private void OnDestroy()
    {
        objectToActive.SetActive(true);
    }
}
