using UnityEngine;

[RequireComponent(typeof(LedgeClimbAS))]

public class LedgeClimbAbility : Ability
{
    public LedgeClimbAS Climb
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Climb = GetComponent<LedgeClimbAS>();

        GetAbilityStates<LedgeClimbAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => true);
        exitConditions.Add(() => false);
    }
}