using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
   public string abilityName = "";

   public abstract void GatherAbility();
   public abstract void Use();
}
