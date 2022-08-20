using System;
using UnityEngine;

public class PlayerState
{
    protected Player Player;
    protected PlayerStatesDescriptor StatesDescriptor;
    protected PlayerStateMachine StateMachine;
    protected PlayerData Data;

    protected Boolean IsAnimationFinished;
    protected Boolean IsActive;

    protected Int32 InputX;
    protected Int32 InputY;

    private readonly String m_AnimBoolName;

    public PlayerState(Player player, PlayerStatesDescriptor statesDescriptor, PlayerStateMachine stateMachine, PlayerData playerData, String animBoolName)
    {
        Player = player;
        StatesDescriptor = statesDescriptor;
        StateMachine = stateMachine;
        Data = playerData;
        m_AnimBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();

        StartAnimation(m_AnimBoolName);
        IsActive = true;

        Debug.Log(m_AnimBoolName);
    }

    public virtual void Exit()
    {
        EndAnimation(m_AnimBoolName);
        IsActive = false;
    }

    public virtual void LogicUpdate()
    {
        if (!IsActive)
        {
            return;
        }

        InputX = Player.InputHandler.NormInputX;
        InputY = Player.InputHandler.NormInputY;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger(Int32 id = 0) { }

    public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;

    protected void ChangeState(PlayerState newState)
    {
        StateMachine.ChangeState(newState);
    }

    protected void StartAnimation(String animationName)
    {
        Player.Animator.SetBool(animationName, true);
        IsAnimationFinished = false;
    }

    protected void EndAnimation(String animationName)
    {
        Player.Animator.SetBool(animationName, false);
        IsAnimationFinished = true;
    }
}
