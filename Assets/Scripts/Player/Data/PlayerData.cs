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
    
    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new(1, 2);

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 endOffset;

    [Header("Endurance")]
    public float minWallClimbEndurance = 2f;
    public float minWallGrabEndurance = 1f;

    public float wallClimbEnduranceFatigueRate = 3f;
    public float wallGrabEnduranceFatigueRate = 1f;

    public float jumpEnduranceCost = 2f;
    public float ledgeClimbEnduranceCost = 2f;

}
