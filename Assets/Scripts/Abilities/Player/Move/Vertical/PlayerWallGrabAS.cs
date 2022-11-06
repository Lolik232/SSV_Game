using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(GrabController), typeof(Movable))]

public class PlayerWallGrabAS : StayAS<PlayerMoveVerticalAbility>
{
	private MoveController _moveController;
	private GrabController _grabController;
	private Movable _movable;

	private PlayerWallClimbAS _climb;
	private PlayerWallSlideAS _slide;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_grabController = GetComponent<GrabController>();
		_movable = GetComponent<Movable>();

		_climb = GetComponent<PlayerWallClimbAS>();
		_slide = GetComponent<PlayerWallSlideAS>();

		bool ClimbCondition() => _grabController.Grab && _moveController.Move.y == 1;

		bool SlideCondition() => _grabController.Grab && _moveController.Move.y == -1 || !_grabController.Grab;

		Transitions.Add(new(_climb, ClimbCondition));
		Transitions.Add(new(_slide, SlideCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = _movable.Velocity.y;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		_movable.SetVelocityY(MoveSpeed);
	}
}
