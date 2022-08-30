using System;
using UnityEngine;

public class PlayerMoveController : MoveController
{
    private Player Player { get; }
    private PlayerData Data { get; set; }

    private int InputX;
    private int InputY;

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

        Player.StatesManager.WallGrabState.EnterEvent += OnWallGrabEnter;
        Player.StatesManager.WallGrabState.ExitEvent += OnWallGrabExit;
        Player.StatesManager.WallSlideState.EnterEvent += OnWallSlideEnter;
        Player.StatesManager.WallSlideState.ExitEvent += OnWallSlideExit;
        Player.StatesManager.WallClimbState.EnterEvent += OnWallClimbEnter;
        Player.StatesManager.WallClimbState.ExitEvent += OnWallClimbExit;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        InputX = Player.InputHandler.NormInputX;
        InputY = Player.InputHandler.NormInputY;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void OnMove()
    {
        CheckIfShouldFlip(InputX);
        SetVelocityX(Data.movementVelocity * InputX);
    }

    private void OnJump()
    {
        SetVelocityY(Player.AbilitiesManager.JumpAbility.JumpVelocity);
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

    public void OnHanging()
    {

    }
}
