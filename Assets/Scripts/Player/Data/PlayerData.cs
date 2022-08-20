using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Input Handler")]
    public Single moveInputTolerance = 0.2f;
    public Single groundSlopeTolerance = 0.01f;
    public Single jumpInputHoldTime = 0.1f;

    [Header("Move State")]
    public Single movementVelocity = 10f;

    [Header("Jump State")]
    public Int32 amountOfJumps = 1;
    public Single jumpVelocity = 16f;

    [Header("In Air State")]
    public Single jumpCoyoteTime = 0.1f;
    public Single vriableJumpHeightMultiplier = 0.5f;

    [Header("Wall Grab State")]
    public Single maxDuration = 5f;
    public Single enduranceGrabLimit = 1f;
    
    [Header("Wall Climb State")]
    public Single wallClimbVelocity = 3f;
    public Single enduranceClimbLimit = 3f;

    [Header("Wall Slide State")]
    public Single wallSlideVelocity = 2f;
   

    [Header("Check Variables")]
    public Single groundCheckRadius = 0.15f;
    public Single groundIsCloseCheckDistance = 0.3f;
    public Single wallCheckDistance = 0.3f;
    public LayerMask whatIsGround;

    [Header("Endurance")]
    public Single climbEnduranceDecreasing = 3f;
    public Single grabEnduranceDecreasing = 1f;
}
