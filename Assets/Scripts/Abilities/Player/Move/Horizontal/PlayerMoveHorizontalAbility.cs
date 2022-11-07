using UnityEngine;

[RequireComponent(typeof(PlayerStayAS), typeof(PlayerMoveForwardAS), typeof(PlayerMoveBackwardAS))]
[RequireComponent(typeof(Movable))]

public class PlayerMoveHorizontalAbility : Ability
{
	private Movable _movable;

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
		_movable = GetComponent<Movable>();

		Forward = GetComponent<PlayerMoveForwardAS>();
		Backward = GetComponent<PlayerMoveBackwardAS>();
		Default = Stay = GetComponent<PlayerStayAS>();

		GetAbilityStates<PlayerMoveHorizontalAbility>();

		IsContinuous = true;
	}

	private void Start()
	{
		bool StayCondition() => !_movable.IsVelocityLocked && !_movable.IsPositionLocked;

		enterConditions.Add(() => StayCondition());
		exitConditions.Add(() => !StayCondition());
	}
}
