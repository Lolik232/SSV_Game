using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : UnitData
{
    [Header("Input Handler")]
    public float moveInputTolerance = 0.2f;
    public float groundSlopeTolerance = 0.01f;
    public float jumpInputHoldTime = 0.1f;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public int amountOfJumps = 1;
    public float jumpVelocity = 16f;

    [Header("In Air State")]
    public float jumpCoyoteTime = 0.1f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Grab State")]
    public float maxDuration = 5f;
    public float minGrabEndurance = 1f;
    
    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;
    public float minClimbEndurance = 2f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 endOffset;

    [Header("Endurance")]
    public float climbEnduranceFatigueRate = 3f;
    public float grabEnduranceFatigueRate = 1f;
}
