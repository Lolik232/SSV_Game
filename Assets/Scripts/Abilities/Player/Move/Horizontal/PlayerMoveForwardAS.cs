using UnityEngine;

[RequireComponent(typeof(MoveController), typeof(Movable), typeof(Rotateable))]

public class PlayerMoveForwardAS : MoveAS<PlayerMoveHorizontalAbility>
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
		bool StayCondition() => _moveController.Move.x != _rotateable.FacingDirection;

		Transitions.Add(new(Ability.Stay, StayCondition));
	}


	protected override void ApplyEnterActions()
	{
		StartSpeed = _movable.Velocity.x;
		MoveDirection = _rotateable.FacingDirection;
		base.ApplyEnterActions();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		_movable.SetVelocityX(MoveSpeed);
	}
}
