using UnityEngine;

[RequireComponent(typeof(PlayerJumpAS), typeof(PlayerWallJumpAS), typeof(CeilChecker))]

public class PlayerJumpAbility : Ability
{
    [SerializeField] private int _amountOfJumps;

    private CeilChecker _ceilChecker;

    public Player Player
    {
        get;
        private set;
    }

    public PlayerJumpAS NormalJump
    {
        get;
        private set;
    }

    public PlayerWallJumpAS WallJump
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
        Player = GetComponent<Player>();

        _ceilChecker = GetComponent<CeilChecker>();

        Default = NormalJump = GetComponent<PlayerJumpAS>();
        WallJump = GetComponent<PlayerWallJumpAS>();

        GetAbilityStates<PlayerJumpAbility>();
    }

    protected override void Start()
    {
        base.Start();
        enterConditions.Add(() => Player.Input.Jump && !Player.IsVelocityLocked && !Player.IsPositionLocked && !_ceilChecker.TouchingCeiling);
        exitConditions.Add(() => false);
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Player.Input.Jump = false;
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
