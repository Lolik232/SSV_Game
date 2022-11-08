using UnityEngine;

[RequireComponent(typeof(PlayerJumpAS), typeof(CeilChecker))]

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

		GetAbilityStates<PlayerJumpAbility>();
	}

	private void Start()
	{
		enterConditions.Add(() => Player.Input.Jump && !Player.IsVelocityLocked && !Player.IsPositionLocked && AmountOfJumps > 0 && !_ceilChecker.TouchingCeiling);
		exitConditions.Add(() => false);
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		AmountOfJumps--;
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		Player.Input.Jump = false;
	}

	public void Restore()
	{
		AmountOfJumps = _amountOfJumps;
	}

	public void SetEmpty()
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
