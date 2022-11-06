using Unity.VisualScripting;

using UnityEngine;

[RequireComponent(typeof(Movable), typeof(PlayerLedgeClimbAS))]

public class PlayerLedgeClimbAbility : Ability
{
	private Movable _movable;

	protected override void Awake()
	{
		base.Awake();
		_movable = GetComponent<Movable>();

		bool StayCondition() => !_movable.IsVelocityLocked && !_movable.IsPositionLocked;

		enterConditions.Add(()=> StayCondition());

		GetAbilityStates<PlayerLedgeClimbAbility>();
	}
}