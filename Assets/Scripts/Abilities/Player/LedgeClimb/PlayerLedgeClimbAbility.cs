using UnityEngine;

[RequireComponent(typeof(PlayerLedgeClimbAS))]

public class PlayerLedgeClimbAbility : Ability
{
	public PlayerLedgeClimbAS Climb
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		Default = Climb = GetComponent<PlayerLedgeClimbAS>();

		GetAbilityStates<PlayerLedgeClimbAbility>();
	}

	private void Start()
	{
		enterConditions.Add(() => true);
		enterConditions.Add(() => false);
	}
}