using System;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : ScriptableObject, IState
{
    protected bool isActive;
    protected float startTime;

    protected List<UnityAction> onStateEnterActions = new();
    protected List<UnityAction> actions = new();
    protected List<UnityAction> onStateExitActions = new();
    protected List<TransitionItem> transitions = new();

    [SerializeField] private VoidEventChannelSO _stateEnterChannel;
    [SerializeField] private VoidEventChannelSO _stateExitChannel;

    protected StateMachine StateMachine { get; private set; }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    public virtual void InitializeStateMachine(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void OnStateEnter()
    {
        isActive = true;
        startTime = Time.time;

        DoChecks();

        foreach (var action in onStateEnterActions)
        {
            action();
        }

        _stateEnterChannel.RaiseEvent();
    }

    public virtual void OnStateExit()
    {
        isActive = false;

        foreach (var action in onStateExitActions)
        {
            action();
        }

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

        foreach (var action in actions)
        {
            action();
        }
    }

    public virtual void OnFixedUpdate()
    {
        DoChecks();
    }

    protected virtual void DoChecks() { }

    protected abstract void TryGetTransitionState(SubStateSO transitionState);
}

public struct TransitionItem
{
    public SubStateSO toState;
    public Func<bool> condition;

    public TransitionItem(SubStateSO toState, Func<bool> condition)
    {
        this.toState = toState;
        this.condition = condition;
    }
}

