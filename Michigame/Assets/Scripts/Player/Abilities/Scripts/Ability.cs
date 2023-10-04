using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class Ability : ScriptableObject
{
    public string name;
    public int id;
    public int level;
    public int currentSouls;
    public int[] lvlUpCondition;
    public float coolDown;
    public float damage;
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
