using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(PlayerWallGrabAS), typeof(PlayerWallClimbAS), typeof(PlayerWallSlideAS))]
[RequireComponent(typeof(Movable), typeof(GrabController))]
[RequireComponent(typeof(MoveController), typeof(Rotateable))]

public class PlayerMoveVerticalAbility : Ability
{
	private Movable _movable;
	private Rotateable _rotateable;
	private GrabController _grabController;
	private MoveController _moveController;

	protected override void Awake()
	{
		base.Awake();
		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();
		_grabController = GetComponent<GrabController>();
		_moveController = GetComponent<MoveController>();


		bool StayCondition() => !_movable.IsVelocityLocked && !_movable.IsPositionLocked &&
															(_grabController.Grab || _moveController.Move.x == _rotateable.FacingDirection);

		enterConditions.Add(() => StayCondition());
		exitConditions.Add(() => !StayCondition());

		GetAbilityStates<PlayerMoveVerticalAbility>();
	}
}
