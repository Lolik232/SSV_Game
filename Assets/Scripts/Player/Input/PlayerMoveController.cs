using System;

using UnityEngine;

public class PlayerMoveController : MoveController
{
    private Player Player { get; }
    private PlayerData Data { get; set; }

    private int m_InputX;
    private int m_InputY;

    private Vector2 m_DetectedLedgePosition;
    private Vector2 m_LedgeClimbStartPosition;
    private Vector2 m_LedgeClimbEndPosition;

    public PlayerMoveController(Player player, PlayerData data) : base(player, data)
    {
        Player = player;
        Data = data;
    }

    public override void Initialize()
    {
        base.Initialize();
        Player.StatesManager.IdleState.EnterEvent += OnIdleEnter;
        Player.StatesManager.MoveState.MoveEvent += OnMove;
        Player.StatesManager.InAirState.MoveEvent += OnMove;

        Player.StatesManager.InAirState.JumpStopEvent += OnJumpStop;
        Player.StatesManager.JumpState.EnterEvent += OnJump;
        Player.StatesManager.WallJumpState.EnterEvent += OnWallJump;

        Player.StatesManager.WallGrabState.EnterEvent += OnWallGrabEnter;
        Player.StatesManager.WallGrabState.ExitEvent += OnWallGrabExit;
        Player.StatesManager.WallSlideState.EnterEvent += OnWallSlideEnter;
        Player.StatesManager.WallSlideState.ExitEvent += OnWallSlideExit;
        Player.StatesManager.WallClimbState.EnterEvent += OnWallClimbEnter;
        Player.StatesManager.WallClimbState.ExitEvent += OnWallClimbExit;


        Player.EnvironmentCheckersManager.IsLedgeDetected.ActiveEvent += DetectLedgePosition;
        Player.StatesManager.LedgeClimbState.EnterEvent += OnLedgeClimbEnter;
        Player.StatesManager.LedgeClimbState.ExitEvent += OnLedgeClimbExit;
        Player.StatesManager.LedgeClimbState.IsClimbing.InactiveEvent += OnLedgeClimbEnd;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_InputX = Player.InputHandler.NormInputX;
        m_InputY = Player.InputHandler.NormInputY;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void OnMove()
    {
        CheckIfShouldFlip(m_InputX);
        SetVelocityX(Data.movementVelocity * m_InputX);
    }

    private void OnJump()
    {
        SetVelocityY(Player.AbilitiesManager.JumpAbility.JumpVelocity);
    }

    private void OnWallJump()
    {
        int wallJumpDirection = Player.EnvironmentCheckersManager.DetermineWallJumpDirection();
        SetVelocity(Player.AbilitiesManager.JumpAbility.WallJumpVelocity,
            Player.AbilitiesManager.JumpAbility.WallJumpAngle,
            wallJumpDirection);
        CheckIfShouldFlip(wallJumpDirection);
    }

    private void OnJumpStop()
    {
        SetVelocityY(CurrentVelocity.y * Player.AbilitiesManager.JumpAbility.VariableJumpHeightMultiplier);
    }

    private void OnIdleEnter()
    {
        SetVelocityX(0f);
    }

    private void OnWallGrabEnter()
    {
        HoldPosition = Player.transform.position;
        NeedToHoldPosition = true;
    }

    private void OnWallGrabExit()
    {
        NeedToHoldPosition = false;
    }

    private void OnWallSlideEnter()
    {
        HoldVelocity.Set(0f, -Player.AbilitiesManager.WallClimbAbility.SlideVelocity);
        NeedToHoldVelocity = true;
    }

    private void OnWallSlideExit()
    {
        NeedToHoldVelocity = false;
    }

    private void OnWallClimbEnter()
    {
        HoldVelocity.Set(0f, Player.AbilitiesManager.WallClimbAbility.ClimbVelocity);
        NeedToHoldVelocity = true;
    }

    private void OnWallClimbExit()
    {
        NeedToHoldVelocity = false;
    }


    private void DetectLedgePosition()
    {
        m_DetectedLedgePosition = Player.transform.position;
    }

    private void OnLedgeClimbEnter()
    {
        MoveToPosition(m_DetectedLedgePosition);

        Vector2 cornerPosition = Player.EnvironmentCheckersManager.DetermineCornerPosition();

        float startX = cornerPosition.x - FacingDirection * Data.startOffset.x;
        float startY = cornerPosition.y - Data.startOffset.y;
        float endX = cornerPosition.x + FacingDirection * Data.endOffset.x;
        float endY = cornerPosition.y + Data.endOffset.y;

        m_LedgeClimbStartPosition.Set(startX, startY);
        m_LedgeClimbEndPosition.Set(endX, endY);

        HoldPosition = m_LedgeClimbStartPosition;
        NeedToHoldPosition = true;
    }

    private void OnLedgeClimbExit()
    {
        NeedToHoldPosition = false;
    }

    public void OnLedgeClimbEnd()
    {
        NeedToHoldPosition = false;
        MoveToPosition(m_LedgeClimbEndPosition);
    }
}
