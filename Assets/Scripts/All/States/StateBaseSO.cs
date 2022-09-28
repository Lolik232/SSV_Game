using System;
using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateBaseSO : ScriptableObject
{
    private bool _isActive;
    protected float startTime;

    protected List<TransitionItem> transitions = new();
    protected List<UnityAction> updateActions = new();
    protected List<UnityAction> enterActions = new();
    protected List<UnityAction> exitActions = new();
    protected List<UnityAction> checks = new();

    [SerializeField] private VoidEventChannelSO _stateEnterChannel;
    [SerializeField] private VoidEventChannelSO _stateExitChannel;

    private StateMachine _machine;

    protected virtual void OnEnable()
    {
        _isActive = false;
        transitions.Clear();
        updateActions.Clear();
        enterActions = new List<UnityAction> { () => { _isActive = true; startTime = Time.time; } };
        exitActions = new List<UnityAction> { () => { _isActive = false; } };
        checks.Clear();
    }

    protected virtual void OnDisable()
    {
    }

    protected void InitializeMachine(StateMachine stateMachine) => _machine = stateMachine;

    public void OnStateEnter()
    {
        if (_isActive) { return; }
        foreach (var action in enterActions) { action(); }
        foreach (var check in checks) { check(); }
        _stateEnterChannel.RaiseEvent();
    }

    public void OnStateExit()
    {
        if (!_isActive) { return; }
        foreach (var action in exitActions) { action(); }
        _stateExitChannel.RaiseEvent();
    }

    public void OnUpdate()
    {
        if (!_isActive) { return; }
        foreach (var transition in transitions)
        {
            if (transition.condition())
            {
                TryGetTransitionState(transition.toState);
                return;
            }
        }

        foreach (var action in updateActions) { action(); }
    }

    public void OnFixedUpdate()
    {
        if (!_isActive) { return; }
        foreach (var check in checks) { check(); }
    }

    protected virtual void TryGetTransitionState(StateSO transitionState) => _machine.GetTransitionState(transitionState);

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
