using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject winScreen;
    public delegate void OnPause();
    public event OnPause onPause;
    public delegate void OnDefeat();
    public event OnDefeat onDefeat;
    public delegate void OnVictory();
    public event OnVictory onVictory;
    private bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        onPause += Pause;
        onDefeat += Defeat;
        onVictory += Victory;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDead)
        {

        }
        if (Input.GetButtonDown("Cancel"))
        {
            onPause();
        }        
        
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void Defeat()
    {

    }

    public void Victory()
    {

    }

}
