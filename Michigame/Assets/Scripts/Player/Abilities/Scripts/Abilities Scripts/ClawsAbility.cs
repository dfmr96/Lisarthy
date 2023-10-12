using UnityEngine;

[CreateAssetMenu(fileName = "ClawsAbilityStats", menuName = "ScriptableObjects/Claw Ability")]
public class ClawsAbility : Ability
{
    public float climbSpeed;
    public float climbingDrag;
    public float exitImpulse;
    public int maxJumps;
    public float jumpForce;
    public float horizontalJumpModifier;
    public float dragWhileJumping;

}
