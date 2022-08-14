using System;
using UnityEngine;

public class PlayerState
{
    protected Player _player;
    protected PlayerStatesDescriptor _statesDescriptor;
    protected PlayerStateMachine _stateMachine;
    protected PlayerData _data;

    protected Boolean _isAnimationFinished;
    protected Boolean _isActive;

    protected Int32 _xInput;
    protected Int32 _yInput;

    protected Single _startTime;

    private readonly String _animBoolName;

    public PlayerState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, String animBoolName)
    {
        _player = player;
        _statesDescriptor = statesDescriptor;
        _stateMachine = stateMachine;
        _data = playerData;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        StartAnimation(_animBoolName);
        _startTime = GetScaledTime();
        _isActive = true;

        Debug.Log(_animBoolName);
    }

    public virtual void Exit()
    {
        EndAnimation(_animBoolName);
        _isActive = false;
    }

    public virtual void LogicUpdate()
    {
        if (!_isActive)
        {
            return;
        }

        _xInput = _player.InputHandler.NormInputX;
        _yInput = _player.InputHandler.NormInputY;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger(Int32 id = 0) { }

    public virtual void AnimationFinishTrigger() => _isAnimationFinished = true;

    protected void ChangeState(PlayerState newState)
    {
        _stateMachine.ChangeState(newState);
    }

    protected void StartAnimation(String animationName)
    {
        _player.Animator.SetBool(animationName, true);
        _isAnimationFinished = false;
    }

    protected void EndAnimation(String animationName)
    {
        _player.Animator.SetBool(animationName, false);
        _isAnimationFinished = true;
    }

    protected Single GetScaledTime()
    {
        return _player.InputHandler.GetScaledTime();
    }

    protected Single GetUnscaledTime()
    {
        return _player.InputHandler.GetUnscaledTime();
    }
}
