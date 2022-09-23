using System.Collections;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Super State")]

public class SuperStateSO : BaseDescriptionSO
{
    private bool _isActive;
    private float _startTime;

    [SerializeField] private StateSO[] _subStates;
    [SerializeField] private TransitionItem[] _transitions;


    [SerializeField] private VoidEventChannelSO _superStateEnterWriter;
    [SerializeField] private VoidEventChannelSO _superStateExitWriter;
    [SerializeField] private VoidEventChannelSO _subStateEnterListener;
    [SerializeField] private StateChangeEventChannelSO _stateChangeWriter;


    private void OnSubStateEnter()
    {
        if (!_isActive)
        {
            OnSuperStateEnter();
        }
    }

    private void OnEnable()
    {
        _subStateEnterListener.OnEventRaised += OnSubStateEnter;
    }

    private void OnDisable()
    {
        _subStateEnterListener.OnEventRaised -= OnSubStateEnter;
    }

    public void OnFixedUpdate()
    {

    }

    private void OnSuperStateEnter()
    {
        _isActive = true;
        _superStateEnterWriter.RaiseEvent();
    }

    private void OnSuperStateExit()
    {
        _isActive = false;
        _superStateExitWriter.RaiseEvent();
    }

    public void OnUpdate()
    {
        if (!_isActive) { return; }

        foreach (var transition in _transitions)
        {
            if (transition.condition.GetStatement())
            {
                _stateChangeWriter.RaiseEvent(transition.toState);
                OnSuperStateExit();
                break;
            }
        }
    }
}
