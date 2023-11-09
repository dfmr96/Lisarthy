using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image mute;
    [SerializeField] private AudioSource music;

    void Start()
    {
        // se mantiene la posicion del slider
        slider.value = PlayerPrefs.GetFloat("MusicAudio", 0.45f);
        music.volume = slider.value;
    }

    public void ChangeVolumen(float value)
    {
        sliderValue= value;
        PlayerPrefs.SetFloat("MusicAudio", sliderValue);
        music.volume = slider.value;
    }
    
}
