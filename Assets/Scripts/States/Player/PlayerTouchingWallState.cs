using UnityEngine;

[RequireComponent(typeof(GroundChecker), typeof(WallChecker), typeof(LedgeChecker))]
[RequireComponent(typeof(GrabController), typeof(MoveController), typeof(Physical))]
[RequireComponent(typeof(Movable))]

public class PlayerTouchingWallState : State
{
	private PlayerGroundedState _grounded;
	private PlayerInAirState _inAir;
	private PlayerOnLedgeState _onLedge;

	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private GrabController _grabController;
	private MoveController _moveController;

	private Physical _physical;
	private Movable _movable;
	private Rotateable _rotateable;

	private PlayerMoveVerticalAbility _moveVertical;

	protected override void Awake()
	{
		base.Awake();
		_grounded = GetComponent<PlayerGroundedState>();
		_inAir = GetComponent<PlayerInAirState>();
		_onLedge = GetComponent<PlayerOnLedgeState>();

		_groundChecker = GetComponent<GroundChecker>();
		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();

		_grabController = GetComponent<GrabController>();
		_moveController = GetComponent<MoveController>();

		_physical = GetComponent<Physical>();
		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();
		_rotateable = GetComponent<Rotateable>();

		_moveVertical = GetComponent<PlayerMoveVerticalAbility>();

		bool GroundedCondition() => _groundChecker.Grounded && (_moveController.Move.y == -1 || !_grabController.Grab);

		bool InAirCondition() => !_wallChecker.TouchingWall || (!_grabController.Grab && _moveController.Move.x != _rotateable.FacingDirection);

		bool OnLedgeCondition() => _wallChecker.TouchingWall && !_ledgeChecker.TouchingLegde;

		Transitions.Add(new(_grounded, GroundedCondition));
		Transitions.Add(new(_inAir, InAirCondition));
		Transitions.Add(new(_onLedge, OnLedgeCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		var holdPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * (_physical.Size.x / 2 + IChecker.CHECK_OFFSET), _physical.Position.y);
		_movable.SetPosition(holdPosition);
		_movable.SetGravity(0f);

		_moveVertical.Unlock();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_movable.ResetGravity();

		_moveVertical.Block();
		_moveVertical.OnExit();
	}
}
