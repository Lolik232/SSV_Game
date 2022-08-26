using System;
using UnityEngine;

public class PlayerAnimationController
{
    public TriggerAction IsAnimationPlaying { get; private set; }
    public String CurrentAnimationName { get; private set; }

    private Animator m_Anim;
    private StateMachine m_StateMachine;


    public PlayerAnimationController(Animator anim, StateMachine stateMachine)
    {
        m_Anim = anim;
        m_StateMachine = stateMachine;

        IsAnimationPlaying = new TriggerAction();
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
