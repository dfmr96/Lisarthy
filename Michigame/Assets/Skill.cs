using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    DoubleJump,
    Fireball,
    Grasps,
    HealthPlus,
}
public class Skill : MonoBehaviour
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private NotificationView _notificationView;
    [SerializeField] private RectTransform healthBG;
    [SerializeField] private RectTransform newHealthBG;
    [SerializeField] private string notification;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out SkillTree skillTree))
        {
            _notificationView.gameObject.SetActive(true);
            _notificationView.ShowNotificacion(notification);
            switch (skillType)
            {
                case SkillType.DoubleJump:
                    skillTree.EnableDoubleJump();
                    break;
                case SkillType.Fireball:
                    skillTree.EnableFireball();
                    break;
                case SkillType.Grasps:
                    skillTree.EnableGrasps();
                    break;
                case SkillType.HealthPlus:
                    healthBG.gameObject.SetActive(false);
                    newHealthBG.gameObject.SetActive(true);
                    break;
                default:
                    //throw new ArgumentOutOfRangeException();
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
