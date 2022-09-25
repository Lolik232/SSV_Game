using System;
using System.Collections;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using UnityEngine;

public class SuperStateSO : StateSO
{
    [SerializeField] protected List<SubStateSO> subStates = new();

    [SerializeField] private VoidEventChannelSO _subStateEnterChannel;

    protected override void OnEnable()
    {
        _subStateEnterChannel.OnEventRaised += OnSubStateEnter;
    }

    protected override void OnDisable()
    {
        _subStateEnterChannel.OnEventRaised -= OnSubStateEnter;
    }

    public override void InitializeStateMachine(StateMachine stateMachine)
    {
        base.InitializeStateMachine(stateMachine);
        foreach (var subState in subStates)
        {
            subState.InitializeStateMachine(stateMachine);
        }
    }

    private void OnSubStateEnter()
    {
        if (!isActive)
        {
            OnStateEnter();
        }
    }

    protected override void TryGetTransitionState(SubStateSO transitionState)
    {
        StateMachine.GetTransitionState(transitionState);
        OnStateExit();
    }
}
