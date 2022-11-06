using Unity.VisualScripting;

using UnityEngine;

[RequireComponent(typeof(PlayerStayAS), typeof(PlayerMoveForwardAS), typeof(PlayerMoveBackwardAS))]
[RequireComponent(typeof(Movable))]

public class PlayerMoveHorizontalAbility : Ability
{
	private Movable _movable;

	protected override void Awake()
	{
		base.Awake();
		_movable = GetComponent<Movable>();

		bool StayCondition() => !_movable.IsVelocityLocked && !_movable.IsPositionLocked;

		enterConditions.Add(() => StayCondition());
		exitConditions.Add(() => !StayCondition());

		GetAbilityStates<PlayerMoveHorizontalAbility>();
	}
}
