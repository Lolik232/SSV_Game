using UnityEngine;

[RequireComponent(typeof(AttackAS))]

public class AttackAbility : Ability
{
    [SerializeField] private float _cooldown;

    public AttackAS Attack
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Attack = GetComponent<AttackAS>();

        GetAbilityStates<AttackAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => Entity.Behaviour.Attack && InactiveTime > _cooldown);
        exitConditions.Add(() => false);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.Behaviour.Attack = false;
    }
}
