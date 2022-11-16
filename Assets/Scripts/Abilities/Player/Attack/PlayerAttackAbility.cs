using UnityEngine;

[RequireComponent(typeof(PlayerAttackAS))]

public class PlayerAttackAbility : PlayerAbility
{
    public PlayerAttackAS Attack
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Attack = GetComponent<PlayerAttackAS>();

        GetAbilityStates<PlayerAttackAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => Player.Input.Attack);
        exitConditions.Add(() => false);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Player.Input.Attack = false;
    }
}
