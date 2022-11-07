using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(GrabController), typeof(Movable))]

public class PlayerWallGrabAS : StayAS<PlayerMoveVerticalAbility>
{
	private MoveController _moveController;
	private GrabController _grabController;
	private Movable _movable;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_grabController = GetComponent<GrabController>();
		_movable = GetComponent<Movable>();
	}

	private void Start()
	{
		bool ClimbCondition() => _grabController.Grab && _moveController.Move.y == 1 && _movable.Velocity.y == 0;

		bool SlideCondition() => (_grabController.Grab && _moveController.Move.y == -1 || !_grabController.Grab) && _movable.Velocity.y == 0;

		Transitions.Add(new(Ability.Climb, ClimbCondition));
		Transitions.Add(new(Ability.Slide, SlideCondition));
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
