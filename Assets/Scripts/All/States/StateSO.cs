using System;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : ScriptableObject, IState
{
    [SerializeField] private string _animBoolName;
    public string AnimBoolName => _animBoolName;

    protected bool isActive;
    protected float startTime;

    protected List<TransitionItem> transitions = new();

    [SerializeField] private VoidEventChannelSO _stateEnterChannel;
    [SerializeField] private VoidEventChannelSO _stateExitChannel;

    protected StateMachine StateMachine { get; private set; }

    protected virtual void OnEnable()
    {
        isActive = false;
        transitions.Clear();
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void Initialize(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void OnStateEnter()
    {
        isActive = true;
        startTime = Time.time;

        DoChecks();

        _stateEnterChannel.RaiseEvent();
    }

    public virtual void OnStateExit()
    {
        isActive = false;

        _stateExitChannel.RaiseEvent();
    }

    public virtual void OnUpdate()
    {
        foreach (var transition in transitions)
        {
            if (!isActive) { return; }
            if (transition.condition())
            {
                TryGetTransitionState(transition.toState);
                return;
            }
        }
    }

    public virtual void OnFixedUpdate()
    {
        DoChecks();
    }

    protected virtual void DoChecks() { }

    protected virtual void TryGetTransitionState(StateSO transitionState) => StateMachine.GetTransitionState(transitionState);

    public virtual void OnAnimationFinishTrigger() { }

    public virtual void OnAnimationTrigger() { }
}

public struct TransitionItem
{
    public StateSO toState;
    public Func<bool> condition;

    public TransitionItem(StateSO toState, Func<bool> condition)
    {
        this.toState = toState;
        this.condition = condition;
    }
}

