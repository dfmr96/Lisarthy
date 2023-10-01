using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    public enum StatToChange
    {
        MaxSpeed,
        MaxAcceleration,
        MaxDeceleration,
        TurnSpeed,
        JumpHeight,
        TimeToApex,
        UpwardMultiplier,
        GravityMultiplier
    }
    [SerializeField] PlayerJump playerJump;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private StatToChange statToChange;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 15;
        slider.minValue = 0;

        switch (statToChange)
        {
            case StatToChange.MaxSpeed:
                SetValue(playerMovement.MaxSpeed);
                break;
            case StatToChange.MaxDeceleration:
                SetValue(playerMovement.MaxDeceleration);
                break;
            case StatToChange.MaxAcceleration:
                SetValue(playerMovement.MaxAcceleration);
                break;
            case StatToChange.TurnSpeed:
                SetValue(playerMovement.TurnSpeed);
                break;
            case StatToChange.JumpHeight:
                SetValue(playerJump.JumpHeight);
                break;
            case StatToChange.TimeToApex:
                SetValue(playerJump.TimeToJumpApex * 10);
                break;
            case StatToChange.UpwardMultiplier:
                SetValue(playerJump.UpwardMultiplier);
                break;
            case StatToChange.GravityMultiplier:
                SetValue(playerJump.GravityMultiplier);
                break;
            
        }
    }

    private void SetValue(float value)
    {
        slider.value = value;
    }

    // Update is called once per frame
    
}
