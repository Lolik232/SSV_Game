using System;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private Int32 m_AmountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
        ResetAmountOfJumpsLeft();
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocityY(Data.jumpVelocity);

        Player.InputHandler.JumpInput.End();
        DecreaseAmountOfJumpsLeft();

        IsAbilityDone = true;
        StatesDescriptor.InAirState.StartJumping();
    }

    public Boolean CanJump() => m_AmountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => m_AmountOfJumpsLeft = Data.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => m_AmountOfJumpsLeft--;

    public void EmptyAmountOfJumpsLeft() => m_AmountOfJumpsLeft = 0;
}
