using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject newGame;
    [SerializeField] private GameObject continueGame;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("GameCreated") == 1)
        {
            continueGame.SetActive(true);
        }
    }
}
