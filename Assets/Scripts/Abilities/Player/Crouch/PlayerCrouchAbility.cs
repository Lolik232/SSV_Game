using UnityEngine;

[RequireComponent(typeof(Crouchable), typeof(PlayerCrouchAS), typeof(PlayerStandAS))]

public class PlayerCrouchAbility : Ability
{
	private Crouchable _crouchable;

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
		_crouchable = GetComponent<Crouchable>();

		Crouch = GetComponent<PlayerCrouchAS>();
		Default = Stand = GetComponent<PlayerStandAS>();

		GetAbilityStates<PlayerCrouchAbility>();

		IsContinuous = true;
	}

	private void Start()
	{
		enterConditions.Add(() => true);
		exitConditions.Add(() => false);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_crouchable.Stand();
	}
}
