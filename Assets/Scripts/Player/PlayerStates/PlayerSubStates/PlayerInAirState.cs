using System;
using UnityEngine;

public class PlayerInAirState : PlayerEnvironmentState
{
    private PlayerAction m_Jumping;

    private PlayerTimeDependentAction m_CoyoteTime;

    private Boolean m_isJumpInputHold;

    public PlayerInAirState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, statesDescriptor, stateMachine, playerData, animBoolName)
    {
        m_CoyoteTime = new PlayerTimeDependentAction(Data.jumpCoyoteTime);
        m_Jumping = new PlayerAction();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        m_isJumpInputHold = Player.InputHandler.IsJumpInputHold;

        CheckJumpMultiplier();

        if (IsGrounded && Player.CurrentVelocity.y < Data.groundSlopeTolerance)
        {
            ChangeState(StatesDescriptor.LandState);
        } 
        else if (JumpInput && StatesDescriptor.JumpState.CanJump())
        {
            Player.InputHandler.JumpInput.End();
            ChangeState(StatesDescriptor.JumpState);
        }
        else if (IsTouchingWall && IsTouchingLedge && GrabInput && StatesDescriptor.WallGrabState.CanGrab())
        {
            ChangeState(StatesDescriptor.WallGrabState);
        }
        else if (IsTouchingWall && InputX == Player.FacingDirection && Player.CurrentVelocity.y <= 0f)
        {
            ChangeState(StatesDescriptor.WallSlideState);
        }
        else
        {
            Player.CheckIfShouldFlip(InputX);

            Player.SetVelocityX(Data.movementVelocity * InputX);

            Player.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.Animator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (m_Jumping.IsActive)
        {
            if (Player.CurrentVelocity.y <= 0f)
            {
                m_Jumping.End();
            }
            else if (!m_isJumpInputHold)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * Data.vriableJumpHeightMultiplier);

                m_Jumping.End();
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void StartJumping() => m_Jumping.Start();

    public void StartCoyoteTime() => m_CoyoteTime.Start();

    private void CheckCoyoteTime()
    {
        if (m_CoyoteTime.IsActive && m_CoyoteTime.IsOutOfTime())
        {
            m_CoyoteTime.End();

            StatesDescriptor.JumpState.DecreaseAmountOfJumpsLeft();
        }
    } 
}
