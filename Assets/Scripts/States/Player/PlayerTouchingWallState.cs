using UnityEngine;

[RequireComponent(typeof(PlayerGroundedState), typeof(PlayerInAirState), typeof(PlayerOnLedgeState))]

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

	private PlayerWallClimbAbility _wallClimb;
	private PlayerWallSlideAbility _wallSlide;
	private PlayerWallGrabAbility _wallGrab;

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

		_wallClimb = GetComponent<PlayerWallClimbAbility>();
		_wallSlide = GetComponent<PlayerWallSlideAbility>();
		_wallGrab = GetComponent<PlayerWallGrabAbility>();

		bool GroundedCondition() => _groundChecker.Grounded && (_moveController.Move.y == -1 || !_grabController.Grab);

		bool InAirCondition() => !_wallChecker.TouchingWall || !_grabController.Grab;

		bool OnLedgeCondition() => _wallChecker.TouchingWall && !_ledgeChecker.TouchingLegde;

		void OnLedgeAction()
		{
			_wallClimb.OnExit();
			_wallSlide.OnExit();
			_wallGrab.OnExit();
		}

		transitions.Add(new StateTransitionItem(_grounded, GroundedCondition));
		transitions.Add(new StateTransitionItem(_inAir, InAirCondition));
		transitions.Add(new StateTransitionItem(_onLedge, OnLedgeCondition, OnLedgeAction));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		var holdPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * (_physical.Size.x / 2 + IChecker.CHECK_OFFSET), _physical.Position.y);
		_movable.SetPosition(holdPosition);
		_movable.SetGravity(0f);

		_wallClimb.Unlock();
		_wallSlide.Unlock();
		_wallGrab.Unlock();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_movable.ResetGravity();

		_wallClimb.Block();
		_wallSlide.Block();
		_wallGrab.Block();

		_wallClimb.OnExit();
		_wallSlide.OnExit();
		_wallGrab.OnExit();
	}
}
