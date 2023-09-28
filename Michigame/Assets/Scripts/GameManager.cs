using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject victoryScreen;
    //public delegate void OnPause();
    //public event OnPause onPause;
    //public delegate void OnDefeat();
    //public event OnDefeat onDefeat;
    //public delegate void OnVictory();
    //public event OnVictory onVictory;
    public bool playerDead = false;
    private float timeScale;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    // Start is called before the first frame update    
    void Start()
    {
        //onPause += Pause;
        //onDefeat += Defeat;
        //onVictory += Victory;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {
            Defeat();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }        
        
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Defeat()
    {
        defeatScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
    }

}
