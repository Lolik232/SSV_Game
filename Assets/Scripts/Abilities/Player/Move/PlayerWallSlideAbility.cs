using UnityEngine;

[RequireComponent(typeof(WallChecker), typeof(Physical))]

public class PlayerWallSlideAbility : MoveAbility
{
	private Vector2 _holdPosition;

	private WallChecker _wallChecker;
	private Physical _physical;

	protected override void Awake()
	{
		base.Awake();
		_wallChecker = GetComponent<WallChecker>();
		_physical = GetComponent<Physical>();
	}

	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		_holdPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * (_physical.Size.x / 2 + 0.01f), _physical.Position.y);
		startSpeed = movable.Velocity.y;
		moveDirection = -1;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		movable.SetPosition(_holdPosition);
		rotateable.RotateBodyIntoDirection(_wallChecker.WallDirection);
		movable.SetGravity(0f);
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
		movable.ResetGravity();
	}
}
