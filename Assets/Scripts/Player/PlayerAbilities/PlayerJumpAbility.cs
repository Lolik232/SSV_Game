using System;
using UnityEngine;

public class PlayerJumpAbility : PlayerAbility
{
    public float JumpVelocity { get; private set; }
    public float VariableJumpHeightMultiplier { get; private set; }

    public int AmountOfJumpsLeft { get; private set; }

    private bool m_CanJump;

    public bool CanJump => AmountOfJumpsLeft > 0 && m_CanJump;

    public PlayerJumpAbility(PlayerAbilitiesManager abilitiesManager, Player player, PlayerData data) : base(abilitiesManager, player, data)
    {
        m_CanJump = new TriggerAction();

        JumpVelocity = data.jumpVelocity;
        VariableJumpHeightMultiplier = data.variableJumpHeightMultiplier;
    }

    public override void Initialize()
    {
        Player.StatesManager.LandState.EnterEvent += ResetAmountOfJumps;
        Player.StatesManager.IdleState.EnterEvent += ResetAmountOfJumps;
        Player.StatesManager.MoveState.EnterEvent += ResetAmountOfJumps;

        Player.StatesManager.InAirState.CoyoteTime.InactiveEvent += BlockJump;
        Player.StatesManager.JumpState.EnterEvent += DecreaseAmountOfJumpsLeft;
    }

    private void ResetAmountOfJumps()
    {
        m_CanJump = true;
        AmountOfJumpsLeft = Data.amountOfJumps;
    }

    private void BlockJump()
    {
        m_CanJump = false;
    }

    public void DecreaseAmountOfJumpsLeft() => AmountOfJumpsLeft--;
}
