using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("after Image")]
    public Single activeTime = 0.1f;
    public Single alphaSet = 0.8f;
    public Single alphMultiplier = 0.85f;


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

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 endOffset;

    [Header("Dash State")]
    public Single dashCooldown = 0.5f;
    public Single maxHoldTime = 1f;
    public Single holdTimeSccale = 0.25f;
    public Single dashTime = 0.2f;
    public Single dashVelocity = 30f;
    public Single drag = 10f;
    public Single dashEndYMultiplier = 0.2f;
    public Single distBetweenAfterImages = 0.5f;

    [Header("Check Variables")]
    public Single groundCheckRadius = 0.15f;
    public Single groundIsCloseCheckDistance = 0.3f;
    public Single wallCheckDistance = 0.3f;
    public LayerMask whatIsGround;


}
