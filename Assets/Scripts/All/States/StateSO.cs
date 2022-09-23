using System;

using All.BaseClasses;
using All.Events;

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Player/States/State")]

public class StateSO : BaseDescriptionSO
{
    [SerializeField] private string _animBoolName;
    public string AnimBoolName => _animBoolName;

    protected bool isActive;
    private float _startTime;

    [SerializeField] private ActionSO[] _onStateEnterActions;
    [SerializeField] private ActionSO[] _onStateExitActions;
    [SerializeField] private ActionSO[] _actions;
    [SerializeField] private TransitionItem[] _transitions;

    [SerializeField] private VoidEventChannelSO _stateEnterWriter;
    [SerializeField] private VoidEventChannelSO _stateExitWriter;
    [SerializeField] private StateChangeEventChannelSO _stateChangeWriter;

    public void OnStateEnter()
    {
        isActive = true;
        _startTime = Time.time;

        foreach (var action in _onStateEnterActions)
        {
            action.Apply();
        }

        _stateEnterWriter.RaiseEvent();
    }

    public void OnStateExit()
    {
        isActive = false;

        foreach (var action in _onStateExitActions)
        {
            action.Apply();
        }

        _stateExitWriter.RaiseEvent();
    }

    public void OnUpdate()
    {
        if (!isActive) { return; }

        foreach (var transition in _transitions)
        {
            if (transition.condition.GetStatement()) {
                _stateChangeWriter.RaiseEvent(transition.toState);
                return;
            }
        }

        foreach (var action in _actions)
        {
            action.Apply();
        }
    }

    public void OnFixedUpdate() { }
}

[Serializable]
public struct TransitionItem
{
    public StateSO toState;
    public ConditionSO condition;
}

