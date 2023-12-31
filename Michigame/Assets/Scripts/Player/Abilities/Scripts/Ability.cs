using UnityEngine;

//[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public int id;
    public KeyCode abilityKey;
    public int level;
    public int currentSouls;
    public int[] lvlUpCondition;
    public float coolDownTime;
    public int damage;
    public float knockBack;
    public float stunDuration;

    public void LevelUp()
    {
        if (currentSouls >= lvlUpCondition[level])
        {
            level++;
            currentSouls -= lvlUpCondition[level-1];            
        }
    }


}
