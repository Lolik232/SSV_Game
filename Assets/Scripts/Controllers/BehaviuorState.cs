using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviuorState : ComponentBase
{
    protected StateMachine Machine
    {
        get;
        private set;
    }
    public List<TransitionItem<State>> Transitions
    {
        get;
        protected set;
    } = new();
    protected dynamic Entity
    {
        get;
        private set;
    }

    protected void Awake()
    {
        Machine = GetComponent<StateMachine>();
        Entity = GetComponent<Entity>();
    }

    public override void OnEnter()
    {
        if (IsActive)
        {
            return;
        }

        ApplyEnterActions();
    }

    public override void OnExit()
    {
        if (!IsActive)
        {
            return;
        }

        ApplyExitActions();
    }

    public override void OnUpdate()
    {
        if (!IsActive)
        {
            return;
        }

        TryGetTransition();

        if (!IsActive)
        {
            return;
        }

        ApplyUpdateActions();
    }

    protected void TryGetTransition()
    {
        foreach (var transition in Transitions)
        {
            if (transition.condition())
            {
                transition.action?.Invoke();
                Machine.GetTransition(transition.target);
                return;
            }
        }
    }
}
