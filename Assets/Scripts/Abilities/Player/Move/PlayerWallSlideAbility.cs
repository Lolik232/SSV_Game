using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(GrabController), typeof(WallChecker))]

public class PlayerWallSlideAbility : MoveAbility
{
	private MoveController _moveController;
	private GrabController _grabController;
	private WallChecker _wallChecker;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_grabController = GetComponent<GrabController>();
		_wallChecker = GetComponent<WallChecker>();

		enterConditions.Add(() => _moveController.Move.y == -1);
		exitConditions.Add(() => _moveController.Move.y != -1);
	}

	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		startSpeed = movable.Velocity.y;
		moveDirection = -1;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		rotateable.RotateBodyIntoDirection(_wallChecker.WallDirection);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.SetVelocityY(moveSpeed);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		rotateable.RotateBodyIntoDirection(rotateable.FacingDirection);
	}
}
