using System;
using UnityEngine;

using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerLedgeClimbState : PlayerEnvironmentState
{
    private Vector2 m_DetectedPosition;
    private Vector2 m_CornerPosition;

    private Vector2 m_StartPosition;
    private Vector2 m_EndPosition;

    private Boolean m_IsHanging;
    private Boolean m_IsClimbing;

    public PlayerLedgeClimbState(PlayerStatesManager statesManager, string animBoolName) : base(statesManager, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        MoveController.SetVelocityZero();
        MoveController.Transform.position = m_DetectedPosition;
        m_CornerPosition = EnvironmentCheckersManager.DetermineCornerPosition();

        Single startX = m_CornerPosition.x - MoveController.FacingDirection * Data.startOffset.x;
        Single startY = m_CornerPosition.y - Data.startOffset.y;
        Single endX = m_CornerPosition.x + MoveController.FacingDirection * Data.endOffset.x;
        Single endY = m_CornerPosition.y + Data.endOffset.y;

        m_StartPosition.Set(startX, startY);
        m_EndPosition.Set(endX, endY);

        MoveController.Transform.position = m_StartPosition;
    }

    public override void Exit()
    {
        base.Exit();

        if (m_IsClimbing)
        {
            MoveController.Transform.position = m_EndPosition;
        }

        m_IsHanging = false;
        m_IsClimbing = false;
    }

    public override void Initialize()
    {
        base.Initialize();

        EnvironmentCheckersManager.IsLedgeDetected.StateChangedEvent += OnLedgeDetectedStateChanged;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (InputX == MoveController.FacingDirection && m_IsHanging && !m_IsClimbing)
        {
            m_IsClimbing = true;

        }
    }

    public void OnLedgeDetectedStateChanged(Boolean isActive)
    {
        m_DetectedPosition = MoveController.Transform.position;
    }

    public void OnLedgeHanged()
    {
        m_IsHanging = true;
    }

    public void OnLedgeClimbed()
    {
        StateMachine.ChangeState(StatesManager.IdleState);
    }

    private void FixPosition(Vector2 position)
    {
        MoveController.SetVelocityZero();
        MoveController.Transform.position = position;
    }
}
