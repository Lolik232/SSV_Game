using System;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private Int32 m_AmountOfJumpsLeft;
    public PlayerJumpState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
        ResetAmountOfJumpsLeft();
    }

    public override void Enter()
    {
        base.Enter();

        MoveController.SetVelocityY(Data.jumpVelocity);

        InputHandler.JumpInput.Terminate();
        DecreaseAmountOfJumpsLeft();

        IsAbilityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public Boolean CanJump() => m_AmountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => m_AmountOfJumpsLeft = Data.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => m_AmountOfJumpsLeft--;
}
