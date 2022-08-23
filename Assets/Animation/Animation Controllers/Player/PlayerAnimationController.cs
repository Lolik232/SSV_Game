using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerStatesManager))]

public class PlayerAnimationController : MonoBehaviour
{
    public TriggerAction IsAnimationPlaying { get; private set; }
    public String CurrentAnimationName { get; private set; }

    private Animator m_Anim;
    private StateMachine m_StateMachine;


    private void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_StateMachine = GetComponent<PlayerStatesManager>().StateMachine;

        m_StateMachine.StateChangedEvent += OnStateChanged;
    }

    private void OnStateChanged(PlayerState state)
    {
        m_Anim.SetBool(CurrentAnimationName, false);
        IsAnimationPlaying.Terminate();
        m_Anim.SetBool(CurrentAnimationName = state.AnimBoolName, true);
        IsAnimationPlaying.Initiate();
    }
}
