using UnityEngine;

[RequireComponent(typeof(PlayerInAirState), typeof(PlayerTouchingWallState), typeof(PlayerOnLedgeState))]

public sealed class PlayerGroundedState : State
{
	private PlayerInAirState _inAir;
	private PlayerTouchingWallState _touchingWall;
	private PlayerOnLedgeState _onLedge;

	private CeilChecker _ceilChecker;
	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private GrabController _grabController;

	private PlayerMoveForwardAbility _moveForward;
	private PlayerMoveBackwardAbility _moveBackward;
	private PlayerStandAbility _stand;

	protected override void Awake()
	{
		base.Awake();
		_inAir = GetComponent<PlayerInAirState>();
		_touchingWall = GetComponent<PlayerTouchingWallState>();
		_onLedge = GetComponent<PlayerOnLedgeState>();

		_groundChecker = GetComponent<GroundChecker>();
		_ceilChecker = GetComponent<CeilChecker>();
		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();

		_grabController = GetComponent<GrabController>();

		_moveForward = GetComponent<PlayerMoveForwardAbility>();
		_moveBackward = GetComponent<PlayerMoveBackwardAbility>();
		_stand = GetComponent<PlayerStandAbility>();

		bool InAirCondition() => !_groundChecker.Grounded;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		 _ledgeChecker.TouchingLegde &&
																		 !_ceilChecker.TouchingCeiling &&
																		 _grabController.Grab;

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

		transitions.Add(new StateTransitionItem(_inAir, InAirCondition));
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
