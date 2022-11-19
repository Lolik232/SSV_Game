using UnityEngine;

[RequireComponent(typeof(CrouchAS), typeof(StandAS))]

public class CrouchAbility : Ability
{
    public CrouchAS Crouch
    {
        get;
        private set;
    }

    public StandAS Stand
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Crouch = GetComponent<CrouchAS>();
        Default = Stand = GetComponent<StandAS>();

        GetAbilityStates<CrouchAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => true);
        exitConditions.Add(() => false);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.Stand();
    }
}
