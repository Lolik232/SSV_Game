using System;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using TMPro.EditorUtilities;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : ScriptableObject
{

    private bool _isActive;
    protected float startTime;

    [Header("Animator")]
    [SerializeField] private List<string> _animBoolNames;
    [SerializeField] private List<string> _animTriggerNames;

    protected List<TransitionItem> transitions = new();
    protected List<UnityAction> updateActions = new();
    protected List<UnityAction> enterActions = new();
    protected List<UnityAction> exitActions = new();
    protected List<UnityAction> animationFinishActions = new();
    protected List<UnityAction> animationActions = new();
    protected List<UnityAction> checks = new();

    private Animator _anim;
    private StateMachine _machine;

    protected virtual void OnEnable()
    {
        _isActive = false;
        startTime = 0f;

        transitions.Clear();
        updateActions.Clear();
        enterActions = new List<UnityAction> { () =>
        {
            _isActive = true;
            startTime = Time.time;
            foreach (var name in _animBoolNames) { _anim.SetBool(name, true); }
            foreach (var name in _animTriggerNames) { _anim.SetTrigger(name); }
        } };
        exitActions = new List<UnityAction> { () =>
        {
            _isActive = false;
            foreach (var name in _animBoolNames) { _anim.SetBool(name, false); }
        } };
        animationFinishActions.Clear();
        animationActions.Clear();
        checks.Clear();
    }

    protected void InitializeMachine(StateMachine stateMachine) => _machine = stateMachine;

    public void OnStateEnter()
    {
        if (_isActive) { return; }
        foreach (var action in enterActions)
        {
            action();
        }
        foreach (var check in checks)
        {
            check();
        }
    }

    public void OnStateExit()
    {
        if (!_isActive) { return; }
        foreach (var action in exitActions)
        {
            action();
        }
    }

    public void OnUpdate()
    {
        foreach (var transition in transitions)
        {
            if (!_isActive) { return; }
            if (transition.condition())
            {
                transition.action?.Invoke();
                _machine.GetTransitionState(transition.toState);
                return;
            }
        }
        foreach (var action in updateActions)
        {
            if (!_isActive) { return; }
            action();
        }
    }

    public void OnFixedUpdate()
    {
        foreach (var check in checks)
        {
            if (!_isActive) { return; }
            check();
        }
    }

    public void OnAnimationFinishTrigger()
    {
        foreach (var action in animationFinishActions)
        {
            if (!_isActive) { return; }
            action();
        }
    }

    public void OnAnimationTrigger()
    {
        foreach (var action in animationFinishActions)
        {
            if (!_isActive) { return; }
            action();
        }
    }
    protected void InitializeAnimator(Animator animator) => _anim = animator;

    protected void SetFloat(string name, float value) => _anim.SetFloat(name, value);
}

public struct TransitionItem
{
    public StateSO toState;
    public Func<bool> condition;
    public UnityAction action;
    public TransitionItem(StateSO toState, Func<bool> condition, UnityAction action = null)
    {
        this.toState = toState;
        this.condition = condition;
        this.action = action;
    }
}