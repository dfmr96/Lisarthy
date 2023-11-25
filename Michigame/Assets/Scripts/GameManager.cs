using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Victory,
    Defeat
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver(GameState state)
    {
        switch (state)
        {
            case GameState.Victory:
                VictoryPanel.SetActive(true);
                break;
            case GameState.Defeat:
                DefeatPanel.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
