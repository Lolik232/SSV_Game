using System.Collections;

using UnityEngine;

[RequireComponent(typeof(CeilChecker), typeof(GroundChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker), typeof(GrabController), typeof(MoveController))]
[RequireComponent(typeof(Rotateable))]

public sealed class PlayerGroundedState : State
{
	[SerializeField] private float _waitForLedgeClimbTime;

	private PlayerInAirState _inAir;
	private PlayerTouchingWallState _touchingWall;
	private PlayerOnLedgeState _onLedge;

	private CeilChecker _ceilChecker;
	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private GrabController _grabController;
	private MoveController _moveController;
	private Rotateable _rotateable;

	private PlayerMoveHorizontalAbility _moveHorizontal;
	private PlayerCrouchAbility _crouch;

	private bool _tryingLedgeClimb;
	private bool _ledgeClimbTimeOut;
	private float _tryingLedgeClimbStartTime;

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
		_moveController = GetComponent<MoveController>();
		_rotateable = GetComponent<Rotateable>();

		_moveHorizontal = GetComponent<PlayerMoveHorizontalAbility>();
		_crouch = GetComponent<PlayerCrouchAbility>();

		bool InAirCondition() => !_groundChecker.Grounded;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		 _ledgeChecker.TouchingLegde &&
																		 !_ceilChecker.TouchingCeiling &&
																		 _grabController.Grab &&
																		 _moveController.Move.y != -1;

		bool OnLedgeCondition() => _ledgeClimbTimeOut;

		void TouchingWallAction()
		{
			_moveHorizontal.OnExit();
		}

		void OnLedgeAction()
		{
			_moveHorizontal.OnExit();
		}

		Transitions.Add(new(_inAir, InAirCondition));
		Transitions.Add(new(_touchingWall, TouchingWallCondition, TouchingWallAction));
		Transitions.Add(new(_onLedge, OnLedgeCondition, OnLedgeAction));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_moveHorizontal.Unlock();

		_tryingLedgeClimb = false;
		_ledgeClimbTimeOut = false;
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_moveHorizontal.Block();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		if (!_tryingLedgeClimb && !_ledgeClimbTimeOut)
		{
			_tryingLedgeClimbStartTime = Time.time;
			StartCoroutine(CheckOnLedgeCondition());
		} 
	}

	private bool CheckIfTryingLedgeClimb()
	{
		return _wallChecker.TouchingWall &&
					 !_ledgeChecker.TouchingLegde && 
					 _moveController.Move.x == _rotateable.FacingDirection;
	}

	private IEnumerator CheckOnLedgeCondition()
	{
		while (_tryingLedgeClimb = CheckIfTryingLedgeClimb())
		{
			if (Time.time > _tryingLedgeClimbStartTime + _waitForLedgeClimbTime)
			{
				_ledgeClimbTimeOut = true;
				_tryingLedgeClimb = false;
				yield break;
			}

			yield return null;
		}
	}
}
