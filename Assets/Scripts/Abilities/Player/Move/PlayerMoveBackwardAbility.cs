using UnityEngine;

[RequireComponent(typeof(MoveController))]

public class PlayerMoveBackwardAbility : MoveAbility
{
	private MoveController _moveController;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
	}
	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		startSpeed = movable.Velocity.x;
		moveDirection = -rotateable.FacingDirection;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.SetVelocityX(moveSpeed);
		rotateable.RotateIntoDirection(_moveController.Move.x);
	}
}
