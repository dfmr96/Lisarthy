using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] Gato playerController;

    public void EnableDoubleJump()
    {
        playerController.doubleJumpTaken = true;
    }

    public void EnableFireball()
    {
        playerController.Tirafuego = true;
    }

    public void EnableGrasps()
    {
        
    }
}
