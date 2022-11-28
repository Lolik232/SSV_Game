using UnityEngine;

[RequireComponent(typeof(DashAS))]

public class DashAbility : Ability
{
    [SerializeField] private int _amountOfDashes;

    [SerializeField] private float _cooldown;

    public DashAS Dash
    {
        get;
        private set;
    }

    public int AmountOfDashes
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Dash = GetComponent<DashAS>();

        GetAbilityStates<DashAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => Entity.Behaviour.Dash && !Entity.IsVelocityLocked && !Entity.IsPositionLocked && InactiveTime > _cooldown && AmountOfDashes > 0 && Entity.IsStanding);
        exitConditions.Add(() => false);
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        DecreaseDashes();
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.Behaviour.Dash = false;
    }

    public void RestoreDashes()
    {
        AmountOfDashes = _amountOfDashes;
    }

    public void SetDashesEmpty()
    {
        AmountOfDashes = 0;
    }

    public void DecreaseDashes()
    {
        if (AmountOfDashes > 0)
        {
            AmountOfDashes--;
        }
    }
}
