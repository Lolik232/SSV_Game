using UnityEngine;

[RequireComponent(typeof(PlayerAttackAS))]

public class PlayerAttackAbility : Ability
{
    public Player Player
    {
        get;
        private set;
    }

    public PlayerAttackAS Attack
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Player = GetComponent<Player>();

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
