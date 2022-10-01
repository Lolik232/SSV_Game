using System;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : ScriptableObject
{
    [SerializeField] private string _animBoolName;

    private bool _isActive;
    protected float startTime;

    protected List<TransitionItem> transitions = new();
    protected List<UnityAction> updateActions = new();
    protected List<UnityAction> enterActions = new();
    protected List<UnityAction> exitActions = new();
    protected List<UnityAction> animationFinishActions = new();
    protected List<UnityAction> animationActions = new();
    protected List<UnityAction> checks = new();

    [SerializeField] private VoidEventChannelSO _stateEnterChannel;
    [SerializeField] private VoidEventChannelSO _stateExitChannel;

    private Animator _anim;
    private StateMachine _machine;

    protected virtual void OnEnable()
    {
        _isActive = false;
        transitions.Clear();
        updateActions.Clear();
        enterActions = new List<UnityAction> { () => { _isActive = true; startTime = Time.time; _anim.SetBool(_animBoolName, true); } };
        exitActions = new List<UnityAction> { () => { _isActive = false; _anim.SetBool(_animBoolName, false); } };
        animationFinishActions.Clear();
        animationActions.Clear();
        checks.Clear();
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
                _machine.GetTransitionState(transition.toState);
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

    public void OnAnimationFinishTrigger()
    {
        if (!_isActive) { return; }
        foreach (var action in animationFinishActions) { action(); }
    }

    public void OnAnimationTrigger()
    {
        if (!_isActive) { return; }
        foreach (var action in animationFinishActions) { action(); }
    }
    protected void InitializeAnimator(Animator animator) => _anim = animator;

    protected void SetBool(string name, bool value) => _anim.SetBool(name, value);
    protected void SetInteger(string name, int value) => _anim.SetInteger(name, value);
    protected void SetFloat(string name, float value) => _anim.SetFloat(name, value);
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

