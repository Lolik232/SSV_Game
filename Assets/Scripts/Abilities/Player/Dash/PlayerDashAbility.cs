using UnityEngine;

[RequireComponent(typeof(PlayerDashAS))]

public class PlayerDashAbility : Ability
{
	[SerializeField] private int _amountOfDashes;


	[SerializeField] private float _cooldown;

	public Player Player
	{
		get;
		private set;
	}

	public PlayerDashAS Dash
	{
		get;
		private set;
	}

	public int AmountOfDashes
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		Player = GetComponent<Player>();

		Default = Dash = GetComponent<PlayerDashAS>();

		GetAbilityStates<PlayerDashAbility>();
	}

	protected override void Start()
	{
		base.Start();
		enterConditions.Add(() => Player.Input.Dash && !Player.IsVelocityLocked && !Player.IsPositionLocked && InactiveTime > _cooldown && AmountOfDashes > 0);
		exitConditions.Add(() => false);
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		DecreaseDashes();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		Player.Input.Dash = false;
	}

	public void RestoreDashes()
	{
		AmountOfDashes = _amountOfDashes;
	}

	public void SetDashesEmpty()
	{
		AmountOfDashes = 0;
	}

	public void DecreaseDashes()
	{
		if (AmountOfDashes > 0)
		{
			AmountOfDashes--;
		}
	}
}
