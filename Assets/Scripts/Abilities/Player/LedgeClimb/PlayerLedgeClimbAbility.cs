using UnityEngine;

[RequireComponent(typeof(PlayerLedgeClimbAS))]

public class PlayerLedgeClimbAbility : Ability
{
	public Player Player
	{
		get; private set;
	}

	public PlayerLedgeClimbAS Climb
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		Player = GetComponent<Player>();

		Default = Climb = GetComponent<PlayerLedgeClimbAS>();

		GetAbilityStates<PlayerLedgeClimbAbility>();
	}

	protected override void Start()
	{
		base.Start();
		enterConditions.Add(() => true);
		exitConditions.Add(() => false);
	}
}