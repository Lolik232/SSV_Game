using System;
using UnityEngine;

public class PlayerMoveController : MoveController
{
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerStatesManager StatesManager { get; private set; }

    private PlayerData m_Data;

    private readonly Player m_Player;

    public PlayerMoveController(Player player) : base(player.transform, player.RB)
    {
        m_Player = player;
    }

    public void SetDependencies()
    {
        InputHandler = m_Player.InputHandler;
        StatesManager = m_Player.StatesManager;
        m_Data = m_Player.Data;
    }

    public override void Initialize()
    {
        base.Initialize();
        StatesManager.IdleState.StandEvent += OnStand;
        StatesManager.MoveState.MoveEvent += OnMove;
        StatesManager.InAirState.MoveEvent += OnMove;
        StatesManager.InAirState.JumpStopEvent += OnJumpStop;
        StatesManager.JumpState.EnterEvent += OnJump;
    }

    private void OnMove(Single velocityX, Int32 direction)
    {
        CheckIfShouldFlip(direction);
        SetVelocityX(velocityX * direction);
    }

    private void OnJump()
    {
        SetVelocityY(m_Data.jumpVelocity);
    }
    
    private void OnJumpStop()
    {
        SetVelocityY(CurrentVelocityY * m_Data.variableJumpHeightMultiplier);
    }

    private void OnStand()
    {
        SetVelocityX(0f);
    }
}
