using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip p_Clip)
    {
        //m_audioSource.clip = p_Clip;
        //m_audioSource.Play();
        m_audioSource.PlayOneShot(p_Clip);
    }
    
    public void StopMusic()
    {
        m_audioSource.Pause();
    }
    public void PlayMusic()
    {
        m_audioSource.Play();
    }
}
