using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(Movable), typeof(Rotateable))]

public class PlayerStayAS : StayAS<PlayerMoveHorizontalAbility>
{
	private MoveController _moveController;
	private Movable _movable;
	private Rotateable _rotateable;

	private PlayerMoveForwardAS _forward;
	private PlayerMoveBackwardAS _backward;

	protected override void Awake()
	{
		base.Awake();
		_moveController = GetComponent<MoveController>();
		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();

		_forward = GetComponent<PlayerMoveForwardAS>();
		_backward = GetComponent<PlayerMoveBackwardAS>();

		bool ForwardCondition() => _moveController.Move.x == _rotateable.FacingDirection;

		bool BackwardCondition() => _moveController.Move.x == -_rotateable.FacingDirection;

		Transitions.Add(new(_forward, ForwardCondition));
		Transitions.Add(new(_backward, BackwardCondition));
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

	public static implicit operator AbilityState<Ability>(PlayerStayAS aS) => aS;
}
