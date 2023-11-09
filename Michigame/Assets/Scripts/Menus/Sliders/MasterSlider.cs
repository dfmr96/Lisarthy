using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterSlider : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image mute;

    void Start()
    {
        // se mantiene la posicion del slider
        slider.value = PlayerPrefs.GetFloat("MasterAudio", 1f);
        AudioListener.volume = slider.value;
    }

    public void ChangeVolumen(float value)
    {
        sliderValue= value;
        PlayerPrefs.SetFloat("MasterAudio", sliderValue);
        AudioListener.volume = slider.value;
    }
}
