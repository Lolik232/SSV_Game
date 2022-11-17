using UnityEngine;

[RequireComponent(typeof(PlayerStayAS), typeof(PlayerMoveForwardAS), typeof(PlayerMoveBackwardAS))]

public class PlayerMoveHorizontalAbility : PlayerAbility
{
    public PlayerMoveForwardAS Forward
    {
        get;
        private set;
    }
    public PlayerMoveBackwardAS Backward
    {
        get;
        private set;
    }
    public PlayerStayAS Stay
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Forward = GetComponent<PlayerMoveForwardAS>();
        Backward = GetComponent<PlayerMoveBackwardAS>();
        Default = Stay = GetComponent<PlayerStayAS>();

        GetAbilityStates<PlayerMoveHorizontalAbility>();
    }

    protected override void Start()
    {
        base.Start();
        bool StayCondition() => !Player.IsVelocityLocked && !Player.IsPositionLocked;

        enterConditions.Add(() => StayCondition());
        exitConditions.Add(() => !StayCondition());
    }
}
