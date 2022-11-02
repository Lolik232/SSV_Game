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

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.TrySetVelocityX(-rotateable.FacingDirection * MoveSpeed);
		rotateable.TryRotateIntoDirection(_moveController.Move.x);
	}
}
