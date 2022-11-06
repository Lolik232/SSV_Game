using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(GrabController), typeof(Movable))]

public class PlayerWallClimbAS : MoveAS<PlayerMoveVerticalAbility>
{
	private MoveController _moveController;
	private GrabController _grabController;
	private Movable _movable;

	private PlayerWallGrabAS _grab;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_grabController = GetComponent<GrabController>();
		_movable = GetComponent<Movable>();

		_grab = GetComponent<PlayerWallGrabAS>();

		bool GrabCondition() => !_grabController.Grab || _moveController.Move.y != 1; 

		Transitions.Add(new(_grab, GrabCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = _movable.Velocity.y;
		MoveDirection = 1;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		_movable.SetVelocityY(MoveSpeed);
	}
}
