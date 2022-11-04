using UnityEngine;

[RequireComponent(typeof(PlayerGroundedState))]

public class PlayerOnLedgeState : State
{
	private PlayerGroundedState _grounded;

	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private GrabController _grabController;
	private MoveController _moveController;

	private Physical _physical;
	private Movable _movable;

	private PlayerLedgeClimbAbility _ledgeClimb;

	private Vector2 _startPosition;
	private Vector2 _endPosition;

	protected override void Awake()
	{
		base.Awake();
		_grounded = GetComponent<PlayerGroundedState>();

		_groundChecker = GetComponent<GroundChecker>();
		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();

		_grabController = GetComponent<GrabController>();
		_moveController = GetComponent<MoveController>();

		_physical = GetComponent<Physical>();
		_movable = GetComponent<Movable>();

		_ledgeClimb = GetComponent<PlayerLedgeClimbAbility>();

		bool GroundedCondition() => !_ledgeClimb.IsActive;

		transitions.Add(new StateTransitionItem(_grounded, GroundedCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_startPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * _physical.Size.x / 2,
																 _ledgeChecker.GroundPosition.y - 1f);
		_endPosition = new Vector2(_wallChecker.WallPosition.x - _wallChecker.WallDirection * _physical.Size.x / 2,
																 _ledgeChecker.GroundPosition.y);
		_movable.SetPosition(_startPosition);
		_movable.SetVelocity(Vector2.zero);
		_movable.SetGravity(0f);

		_ledgeClimb.Unlock();
		_ledgeClimb.OnEnter();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_movable.SetPosition(_endPosition);
		_movable.ResetGravity();

		_ledgeClimb.Block();
	}
}
