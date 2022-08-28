using System;

using UnityEngine;

public class PlayerInAirState : PlayerEnvironmentState
{
    private TriggerAction m_Jumping;

    private TimeDependentAction m_CoyoteTime;
    private Boolean m_IsJumpInputHold;

    public PlayerInAirState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
        m_CoyoteTime = new TimeDependentAction(Data.jumpCoyoteTime);
        m_Jumping = new TriggerAction();
    }

    public event Action JumpStopEvent;
    public event Action CoyoteTimeEndEvent;

    public override void Initialize()
    {
        base.Initialize();

        StatesManager.JumpState.EnterEvent += m_Jumping.Initiate;
        StatesManager.IdleState.GroundLeaveEvent += m_CoyoteTime.Initiate;
        StatesManager.MoveState.GroundLeaveEvent += m_CoyoteTime.Initiate;
        StatesManager.LandState.GroundLeaveEvent += m_CoyoteTime.Initiate;
    }

    public override void Enter()
    {
        base.Enter();

        m_IsJumpInputHold = InputHandler.JumpInputHold;

        InputHandler.JumpInputHold.StateChangedEvent += SetIsJumpInputHold;
        m_CoyoteTime.StateChangedEvent += OnCoyouteTimeEnd;
    }

    public override void Exit()
    {
        base.Exit();

        InputHandler.JumpInputHold.StateChangedEvent -= SetIsJumpInputHold;
        m_CoyoteTime.StateChangedEvent -= OnCoyouteTimeEnd;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckIfJumpEnd();
        CheckJumpHold();

        if (IsGrounded && VelocityY < Data.groundSlopeTolerance)
        {
            StateMachine.ChangeState(StatesManager.LandState);
        }
        else if (JumpInput && AbilitiesManager.JumpAbility.CanJump)
        {
            StateMachine.ChangeState(StatesManager.JumpState);
        }
        else if (IsTouchingWall && IsTouchingLedge && GrabInput)
        {
            StateMachine.ChangeState(StatesManager.WallGrabState);
        }
        else if (IsTouchingWall && InputX == MoveController.FacingDirection && VelocityY <= 0f)
        {
            StateMachine.ChangeState(StatesManager.WallSlideState);
        }
        else
        {
            SendMove(Data.movementVelocity, InputX);
        }
    }

    protected void SendJumpStop()
    {
        JumpStopEvent?.Invoke();
    }

    protected void SendCoyoteTimeEnd()
    {
        CoyoteTimeEndEvent?.Invoke();
    }

    private void CheckJumpHold()
    {
        if (IsActive && m_Jumping.IsActive && !m_IsJumpInputHold && VelocityY > 0f)
        {
            SendJumpStop();
            m_Jumping.Terminate();
        }
    }

    private void CheckIfJumpEnd()
    {
        if (m_Jumping.IsActive && VelocityY <= 0f)
        {
            m_Jumping.Terminate();
        }
    }

    private void OnCoyouteTimeEnd(Boolean isActive)
    {
        if (!isActive)
        {
            SendCoyoteTimeEnd();
        }
    }

    private void SetIsJumpInputHold(Boolean isActive) => m_IsJumpInputHold = isActive;


}
