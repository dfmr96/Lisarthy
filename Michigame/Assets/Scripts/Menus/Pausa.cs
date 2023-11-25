using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            canvas.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            MusicManager.Instance.StopMusic();
            AudioManager.Instance.StopMusic();
        }
        else if (isPaused &&Input.GetKeyDown(KeyCode.Escape) )
        {
            ClosePauseMenu();
        }
    }

    public void ClosePauseMenu()
    {
        canvas.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            MusicManager.Instance.PlayMusic();
            AudioManager.Instance.PlayMusic();
    }
}
