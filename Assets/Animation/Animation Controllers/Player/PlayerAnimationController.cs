using System;

using UnityEngine;

public class PlayerAnimationController
{
    public TriggerAction IsAnimationPlaying { get; private set; }
    public String CurrentAnimationName { get; private set; }

    private Animator m_Anim;
    private StateMachine m_StateMachine;

    private readonly Player m_Player;

    public PlayerAnimationController(Player player)
    {
        m_Player = player;
        IsAnimationPlaying = new TriggerAction();
    }

    public void SetDependencies()
    {
        m_Anim = m_Player.Anim;
        m_StateMachine = m_Player.StatesManager.StateMachine;
    }

    public void Initialize()
    {
        m_StateMachine.StateChangedEvent += OnStateChanged;
    }

    ~PlayerAnimationController()
    {
        m_StateMachine.StateChangedEvent -= OnStateChanged;
    }

    private void OnStateChanged(PlayerState state)
    {
        m_Anim.SetBool(CurrentAnimationName, false);
        IsAnimationPlaying.Terminate();
        m_Anim.SetBool(CurrentAnimationName = state.AnimBoolName, true);
        IsAnimationPlaying.Initiate();
    }
}
