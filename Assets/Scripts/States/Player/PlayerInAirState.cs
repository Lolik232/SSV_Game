using UnityEngine;

[RequireComponent(typeof(PlayerGroundedState), typeof(PlayerTouchingWallState), typeof(PlayerOnLedgeState))]

public sealed class PlayerInAirState : State
{
	private PlayerGroundedState _grounded;
	private PlayerTouchingWallState _touchingWall;
	private PlayerOnLedgeState _onLedge;

	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private GrabController _grabController;
	private MoveController _moveController;

	private Rotateable _rotateable;
	private Movable _movable;

	private PlayerMoveForwardAbility _moveForward;
	private PlayerMoveBackwardAbility _moveBackward;
	private PlayerStandAbility _stand;

	protected override void Awake()
	{
		base.Awake();
		_grounded = GetComponent<PlayerGroundedState>();
		_touchingWall = GetComponent<PlayerTouchingWallState>();
		_onLedge = GetComponent<PlayerOnLedgeState>();

		_groundChecker = GetComponent<GroundChecker>();
		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();

		_grabController = GetComponent<GrabController>();
		_moveController = GetComponent<MoveController>();

		_rotateable = GetComponent<Rotateable>();
		_movable = GetComponent<Movable>();

		_moveForward = GetComponent<PlayerMoveForwardAbility>();
		_moveBackward = GetComponent<PlayerMoveBackwardAbility>();
		_stand = GetComponent<PlayerStandAbility>();

		bool GroundedCondition() => _groundChecker.Grounded && _movable.Velocity.y < 0.01f;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		 _ledgeChecker.TouchingLegde &&
																		 (_grabController.Grab || _moveController.Move.x == _rotateable.FacingDirection && _movable.Velocity.y < 0.01f);

		bool OnLedgeCondition() => _wallChecker.TouchingWall && !_ledgeChecker.TouchingLegde;

		void TouchingWallAction()
		{
			_moveForward.OnExit();
			_moveBackward.OnExit();
			_stand.OnExit();
		}

		void OnLedgeAction()
		{
			_moveForward.OnExit();
			_moveBackward.OnExit();
			_stand.OnExit();
		}

		transitions.Add(new StateTransitionItem(_grounded, GroundedCondition));
		transitions.Add(new StateTransitionItem(_touchingWall, TouchingWallCondition, TouchingWallAction));
		transitions.Add(new StateTransitionItem(_onLedge, OnLedgeCondition, OnLedgeAction));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_moveForward.Unlock();
		_moveBackward.Unlock();
		_stand.Unlock();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_moveForward.Block();
		_moveBackward.Block();
		_stand.Block();
	}
}
