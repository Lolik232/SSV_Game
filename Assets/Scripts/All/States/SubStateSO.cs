using System.Collections;
using System.Collections.Generic;
using All.Events;

using UnityEngine;

public class SubStateSO : StateSO
{
    [SerializeField] private string _animBoolName;
    public string AnimBoolName => _animBoolName;

    [SerializeField] private VoidEventChannelSO _subStateEnterChannel = default;

    protected override void TryGetTransitionState(SubStateSO transitionState)
    {
        StateMachine.GetTransitionState(transitionState);
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _subStateEnterChannel?.RaiseEvent();
    }

    public virtual void OnAnimationFinishTrigger() { }

    public virtual void OnAnimationTrigger() { }
}
