using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerStatesManager))]

public class PlayerAnimationController : MonoBehaviour
{
    public TriggerAction IsAnimationPlaying { get; private set; }
    public String CurrentAnimationName { get; private set; }

    private Animator m_Anim;
    private StateMachine m_StateMachine;
    private PlayerStatesManager m_StateManager;


    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_StateManager = GetComponent<PlayerStatesManager>();

        IsAnimationPlaying = new TriggerAction();
    }

    private void Start()
    {
        m_StateMachine = m_StateManager.StateMachine;

        m_StateMachine.StateChangedEvent += OnStateChanged;
    }

    private void OnDestroy()
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
