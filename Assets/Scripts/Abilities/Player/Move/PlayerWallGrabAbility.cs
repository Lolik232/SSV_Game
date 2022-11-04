using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(GrabController))]

public class PlayerWallGrabAbility : MoveStopAbility
{
	private MoveController _moveController;
	private GrabController _grabController;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_grabController = GetComponent<GrabController>();

		enterConditions.Add(() => _moveController.Move.y == 0 && _grabController.Grab);
		exitConditions.Add(() => _moveController.Move.y != 0 || !_grabController.Grab);
	}

	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		startSpeed = movable.Velocity.y;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.SetVelocityY(moveSpeed);
	}
}
