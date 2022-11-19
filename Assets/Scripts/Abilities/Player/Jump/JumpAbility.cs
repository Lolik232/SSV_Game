using UnityEngine;

[RequireComponent(typeof(JumpAS), typeof(WallJumpAS))]

public class JumpAbility : Ability
{
    [SerializeField] private int _amountOfJumps;

    public JumpAS NormalJump
    {
        get;
        private set;
    }

    public WallJumpAS WallJump
    {
        get;
        private set;
    }

    public int AmountOfJumps
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = NormalJump = GetComponent<JumpAS>();
        WallJump = GetComponent<WallJumpAS>();

        GetAbilityStates<JumpAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => Entity.Behaviour.Jump && !Entity.IsVelocityLocked && !Entity.IsPositionLocked && !Entity.TouchingCeiling);
        exitConditions.Add(() => false);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Entity.Behaviour.Jump = false;
    }

    public void RestoreJumps()
    {
        AmountOfJumps = _amountOfJumps;
    }

    public void SetJumpsEmpty()
    {
        AmountOfJumps = 0;
    }

    public void DecreaseJumps()
    {
        if (AmountOfJumps > 0)
        {
            AmountOfJumps--;
        }
    }
}
