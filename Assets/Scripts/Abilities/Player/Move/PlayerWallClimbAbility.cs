using UnityEngine;

[RequireComponent(typeof(WallChecker), typeof(Physical))]

public class PlayerWallClimbAbility : MoveAbility
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
		_holdPosition = new Vector2(_wallChecker.WallPosition.x - rotateable.FacingDirection * (_physical.Size.x / 2 + 0.01f), _physical.Position.y);
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		movable.TrySetPosition(_holdPosition);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		movable.TrySetVelocityY(MoveSpeed);
	}
}
