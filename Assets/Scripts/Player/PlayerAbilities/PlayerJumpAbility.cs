using System;
using UnityEngine;

public class PlayerJumpAbility : PlayerAbility
{
    public float JumpVelocity { get; private set; }

    public Vector2 WallJumpAngle { get; private set; }

    public float WallJumpVelocity { get; private set; }

    public float VariableJumpHeightMultiplier { get; private set; }

    public int AmountOfJumpsLeft { get; private set; }

    private bool m_CanJump;

    public bool CanJump => AmountOfJumpsLeft > 0 && m_CanJump && Player.CharacteristicsManager.Endurance.Current >= JumpEnduranceCost;
    public bool CanWallJump => Player.CharacteristicsManager.Endurance.Current >= JumpEnduranceCost;

    public float JumpEnduranceCost { get; private set; }

    public PlayerJumpAbility(PlayerAbilitiesManager abilitiesManager, Player player, PlayerData data) : base(abilitiesManager, player, data)
    {
        m_CanJump = new TriggerAction();

        JumpVelocity = data.jumpVelocity;
        VariableJumpHeightMultiplier = data.variableJumpHeightMultiplier;
        WallJumpAngle = data.wallJumpAngle;
        WallJumpVelocity = data.wallJumpVelocity;
        JumpEnduranceCost = data.jumpEnduranceCost;
    }

    public override void Initialize()
    {
        Player.StatesManager.LandState.EnterEvent += OnLandEnter;
        Player.StatesManager.IdleState.EnterEvent += OnIdleEnter;
        Player.StatesManager.MoveState.EnterEvent += OnMoveEnter;

        Player.StatesManager.InAirState.JumpCoyoteTime.InactiveEvent += OnJumpCoyoteTimeEnd;
        Player.StatesManager.JumpState.EnterEvent += OnJump;
        Player.StatesManager.WallJumpState.EnterEvent += OnWallJump;
    }

    private void OnIdleEnter()
    {
        ResetAmountOfJumps();
    }

    private void OnLandEnter()
    {
        ResetAmountOfJumps();
    }

    private void OnMoveEnter()
    {
        ResetAmountOfJumps();
    }

    private void OnJump()
    {
        DecreaseAmountOfJumpsLeft();
    }

    private void OnWallJump()
    {
        ResetAmountOfJumps();
        DecreaseAmountOfJumpsLeft();
    }

    private void ResetAmountOfJumps()
    {
        m_CanJump = true;
        AmountOfJumpsLeft = Data.amountOfJumps;
    }

    private void OnJumpCoyoteTimeEnd()
    {
        m_CanJump = false;
    }

    public void DecreaseAmountOfJumpsLeft() => AmountOfJumpsLeft--;
}
