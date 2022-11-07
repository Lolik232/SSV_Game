using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(Movable), typeof(Rotateable))]

public class PlayerStayAS : StayAS<PlayerMoveHorizontalAbility>
{
	private MoveController _moveController;
	private Movable _movable;
	private Rotateable _rotateable;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();
	}

	private void Start()
	{
		bool ForwardCondition() => _moveController.Move.x == _rotateable.FacingDirection && _movable.Velocity.x == 0;

		bool BackwardCondition() => _moveController.Move.x == -_rotateable.FacingDirection && _movable.Velocity.x == 0;

		Transitions.Add(new(Ability.Forward, ForwardCondition));
		Transitions.Add(new(Ability.Backward, BackwardCondition));
	}

	protected override void ApplyEnterActions()
	{
		StartSpeed = _movable.Velocity.x;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		_movable.SetVelocityX(MoveSpeed);
	}
}
