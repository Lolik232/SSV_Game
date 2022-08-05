using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public Single movementVelocity = 10f;

    [Header("Jump State")]
    public Single jumpVelocity = 15f;
    public Int32 amountOfJumps = 1;

    [Header("In Air State")]
    public Single coyoteTime = 0.2f;
    public Single variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public Single wallSlideVelocity = 3f;

    [Header("Check Variables")]
    public Single groundCheckRadius = 0.15f;
    public Single wallCheckDistance = 0.3f;
    public LayerMask whatIsGround;


}
