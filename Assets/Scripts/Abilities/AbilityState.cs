using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityState<AbilityT> : StateBase where AbilityT : Ability
{
    [SerializeField] protected AudioClip _clip;

    public AbilityT Ability { get; private set; }
    public List<TransitionItem<AbilityState<AbilityT>>> Transitions { get; protected set; } = new();

    protected dynamic Entity { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Ability = GetComponent<AbilityT>();
    }

    protected override void Start()
    {
        base.Start();
        Entity = Ability.Entity;
    }

    protected override void TryGetTransition()
    {
        foreach (var transition in Transitions)
        {
            if (transition.condition())
            {
                transition.action?.Invoke();
                Ability.GetTransition(transition.target);
                return;
            }
        }
    }
}