using System.Collections.Generic;
using System.Linq;
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
        foreach (var transition in Transitions.Where(transition => transition.condition()))
        {
            transition.actions.ForEach(a => a?.Invoke());
            Machine.GetTransition(transition.target);
            return;
        }
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        Entity.DoChecks();
    }
}