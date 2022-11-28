using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public abstract class State : StateBase
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

    protected override void Awake()
    {
        base.Awake();
        Machine = GetComponent<StateMachine>();
        Entity = GetComponent<Entity>();
    }

    protected override void TryGetTransition()
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

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Entity.DoChecks();
    }
}