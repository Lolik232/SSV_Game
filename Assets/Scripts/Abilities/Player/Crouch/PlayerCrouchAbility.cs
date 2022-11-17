using UnityEngine;

[RequireComponent(typeof(PlayerCrouchAS), typeof(PlayerStandAS))]

public class PlayerCrouchAbility : PlayerAbility
{
    public PlayerCrouchAS Crouch
    {
        get;
        private set;
    }

    public PlayerStandAS Stand
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Crouch = GetComponent<PlayerCrouchAS>();
        Default = Stand = GetComponent<PlayerStandAS>();

        GetAbilityStates<PlayerCrouchAbility>();
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
        Player.Stand();
    }
}
