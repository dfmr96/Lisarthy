using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    DoubleJump,
    Fireball,
    Grasps
}
public class Skill : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out SkillTree skillTree))
        {
            switch (skillType)
            {
                case SkillType.DoubleJump:
                    skillTree.EnableDoubleJump();
                    break;
                case SkillType.Fireball:
                    skillTree.EnableFireball();
                    break;
                case SkillType.Grasps:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Destroy(gameObject);
        }
    }
}
