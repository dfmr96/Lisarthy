using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [SerializeField] PlayerJump _playerJump;
    [SerializeField] private PlayerMovement _playerMovement;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 20;
        slider.minValue = 0;

        slider.value = _playerMovement.MaxSpeed;
    }

    // Update is called once per frame
    public void SetDataSpeed()
    {
        _playerMovement.SetMaxSpeed(slider.value);
    }
    
    public void SetDataAccel()
    {
        _playerMovement.SetMaxAccel(slider.value);
    }

    public void SetJumpHeight()
    {
        _playerJump.SetJumpHeight(slider.value);
    }
}
