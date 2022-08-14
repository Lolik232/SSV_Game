using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Input Handler")]
    public Single moveInputTolerance = 0.2f;
    public Single groundSlopeTolerance = 0.01f;

    [Header("Move State")]
    public Single movementVelocity = 10f;

    [Header("Jump State")]
    public Single jumpCoyoteTime = 0.1f;

    [Header("Check Variables")]
    public Single groundCheckRadius = 0.15f;
    public Single groundIsCloseCheckDistance = 0.3f;
    public Single wallCheckDistance = 0.3f;
    public LayerMask whatIsGround;
}
