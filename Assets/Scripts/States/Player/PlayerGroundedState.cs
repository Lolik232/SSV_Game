using System.Collections;

using UnityEngine;

[RequireComponent(typeof(CeilChecker), typeof(GroundChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker), typeof(GrabController), typeof(MoveController))]
[RequireComponent(typeof(Rotateable), typeof(Crouchable))]

public sealed class PlayerGroundedState : State
{
	[SerializeField] private float _waitForLedgeClimbTime;

	private PlayerInAirState _inAir;
	private PlayerTouchingWallState _touchingWall;
	private PlayerOnLedgeState _onLedge;

	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private GrabController _grabController;
	private MoveController _moveController;
	private Rotateable _rotateable;
	private Crouchable _crouchable;

	private PlayerMoveHorizontalAbility _moveHorizontal;
	private PlayerJumpAbility _jump;
	private PlayerCrouchAbility _crouch;

	private bool _tryingLedgeClimb;
	private bool _ledgeClimbTimeOut;
	private float _tryingLedgeClimbStartTime;

	private float TryingLedgeClimbTime
	{
		get => Time.time - _tryingLedgeClimbStartTime;
		set => _tryingLedgeClimbStartTime = value;
	}

	protected override void Awake()
	{
		base.Awake();
		_inAir = GetComponent<PlayerInAirState>();
		_touchingWall = GetComponent<PlayerTouchingWallState>();
		_onLedge = GetComponent<PlayerOnLedgeState>();

		_groundChecker = GetComponent<GroundChecker>();
		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();

		_grabController = GetComponent<GrabController>();
		_moveController = GetComponent<MoveController>();
		_rotateable = GetComponent<Rotateable>();
		_crouchable = GetComponent<Crouchable>();

		_moveHorizontal = GetComponent<PlayerMoveHorizontalAbility>();
		_crouch = GetComponent<PlayerCrouchAbility>();
		_jump = GetComponent<PlayerJumpAbility>();
	}

	private void Start()
	{
		bool InAirCondition() => !_groundChecker.Grounded;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		 _ledgeChecker.TouchingLegde &&
																		 _crouchable.IsStanding &&
																		 _grabController.Grab &&
																		 _moveController.Move.y != -1;

		bool OnLedgeCondition() => _ledgeClimbTimeOut;

		void InAirAction()
		{
			if (!_jump.IsActive)
			{
				StartCoroutine(_inAir.CheckJumpCoyoteTime());
			}
		}

		void TouchingWallAction()
		{
			_jump.SetEmpty();
			_moveHorizontal.SetEmpty();
		}

		void OnLedgeAction()
		{
			_jump.SetEmpty();
			_moveHorizontal.SetEmpty();
		}

		Transitions.Add(new(_inAir, InAirCondition, InAirAction));
		Transitions.Add(new(_touchingWall, TouchingWallCondition, TouchingWallAction));
		Transitions.Add(new(_onLedge, OnLedgeCondition, OnLedgeAction));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_jump.Restore();
		_moveHorizontal.Restore();
		_crouch.Restore();

		_tryingLedgeClimb = false;
		_ledgeClimbTimeOut = false;
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_crouch.SetEmpty();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		if (!_tryingLedgeClimb && !_ledgeClimbTimeOut)
		{
			TryingLedgeClimbTime = Time.time;
			StartCoroutine(CheckOnLedgeCondition());
		}
	}

	private bool CheckIfTryingLedgeClimb()
	{
		return _wallChecker.TouchingWall &&
					 !_ledgeChecker.TouchingLegde &&
					 (_moveController.Move.x == _rotateable.FacingDirection ||
						_moveController.Move.y == 1);
	}

	private IEnumerator CheckOnLedgeCondition()
	{
		while (IsActive && (_tryingLedgeClimb = CheckIfTryingLedgeClimb()))
		{
			yield return null;

			if (TryingLedgeClimbTime > _waitForLedgeClimbTime)
			{
				_ledgeClimbTimeOut = true;
				_tryingLedgeClimb = false;
				yield break;
			}
		}
	}
}
