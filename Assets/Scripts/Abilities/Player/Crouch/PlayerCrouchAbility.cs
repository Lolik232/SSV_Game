using UnityEngine;

[RequireComponent(typeof(PlayerCrouchAS), typeof(PlayerStandAS))]

public class PlayerCrouchAbility : Ability
{
	public Player Player
	{
		get;
		private set;
	}

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
		Player = GetComponent<Player>();

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
