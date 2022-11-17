using UnityEngine;

[RequireComponent(typeof(PlayerLedgeClimbAS))]

public class PlayerLedgeClimbAbility : PlayerAbility
{
    public PlayerLedgeClimbAS Climb
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Climb = GetComponent<PlayerLedgeClimbAS>();

        GetAbilityStates<PlayerLedgeClimbAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => true);
        exitConditions.Add(() => false);
    }
}