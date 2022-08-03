using System;
using UnityEngine;

public class PlayerState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected PlayerData _playerData;

    protected Single _startTime;

    private String _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, String animBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _playerData = playerData;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        _player.Anim.SetBool(_animBoolName, true);
        _startTime = Time.time;

        Debug.Log(_animBoolName);
    }

    public virtual void Exit()
    {
        _player.Anim.SetBool(_animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
