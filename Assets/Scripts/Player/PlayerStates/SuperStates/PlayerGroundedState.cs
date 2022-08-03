using System;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Int32 _xInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, String animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

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

        _xInput = _player.InputHandler.NormInutX;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
