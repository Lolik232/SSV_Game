using UnityEngine;

[RequireComponent(typeof(StayAS), typeof(MoveForwardAS), typeof(MoveBackwardAS))]

public class MoveHorizontalAbility : Ability
{
    public MoveForwardAS Forward
    {
        get;
        private set;
    }
    public MoveBackwardAS Backward
    {
        get;
        private set;
    }
    public StayAS Stay
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Forward = GetComponent<MoveForwardAS>();
        Backward = GetComponent<MoveBackwardAS>();
        Default = Stay = GetComponent<StayAS>();

        GetAbilityStates<MoveHorizontalAbility>();
    }

    protected override void Start()
    {
        base.Start();
        bool StayCondition() => !Entity.IsVelocityLocked && !Entity.IsPositionLocked;

        enterConditions.Add(() => StayCondition());
        exitConditions.Add(() => !StayCondition());
    }
}
