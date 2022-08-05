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

    [Header("Wall Jump State")]
    public Single wallJumpVelocity = 20f;
    public Single wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new(1, 2);

    [Header("In Air State")]
    public Single coyoteTime = 0.2f;
    public Single variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public Single wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    public Single wallClimbVelocity = 3f;

    [Header("Check Variables")]
    public Single groundCheckRadius = 0.15f;
    public Single wallCheckDistance = 0.3f;
    public LayerMask whatIsGround;


}
