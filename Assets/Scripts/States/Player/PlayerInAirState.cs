using System.Collections;

using UnityEngine;

[RequireComponent(typeof(GroundChecker), typeof(WallChecker), typeof(LedgeChecker))]
[RequireComponent(typeof(GrabController), typeof(MoveController), typeof(Movable))]
[RequireComponent(typeof(Rotateable))]

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

	private Movable _movable;
	private Rotateable _rotateable;

	private PlayerMoveHorizontalAbility _moveHorizontal;
	private PlayerMoveVerticalAbility _moveVertical;
	private PlayerJumpAbility _jump;

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

		_movable = GetComponent<Movable>();
		_rotateable = GetComponent<Rotateable>();

		_moveHorizontal = GetComponent<PlayerMoveHorizontalAbility>();
		_moveVertical = GetComponent<PlayerMoveVerticalAbility>();
		_jump = GetComponent<PlayerJumpAbility>();
	}

	private void Start()
	{
		bool GroundedCondition() => _groundChecker.Grounded && _movable.Velocity.y < 0.01f;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		 _ledgeChecker.TouchingLegde &&
																		 (_grabController.Grab || _moveController.Move.x == _rotateable.FacingDirection);

		bool OnLedgeCondition() => _wallChecker.TouchingWall && !_ledgeChecker.TouchingLegde;

		void TouchingWallAction()
		{
			_moveHorizontal.SetEmpty();
			_moveVertical.Restore();
			if (_grabController.Grab)
			{
				_moveVertical.OnEnter(_moveVertical.Grab);
			}
			else
			{
				_moveVertical.OnEnter(_moveVertical.Slide);
			}
		}

		void OnLedgeAction()
		{
			_moveHorizontal.SetEmpty();
		}

		Transitions.Add(new(_grounded, GroundedCondition));
		Transitions.Add(new(_touchingWall, TouchingWallCondition, TouchingWallAction));
		Transitions.Add(new(_onLedge, OnLedgeCondition, OnLedgeAction));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_moveHorizontal.Restore();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
	}

	public IEnumerator CheckJumpCoyoteTime()
	{
		while (IsActive && ActiveTime < _jump.Jump.CoyoteTime)
		{
			yield return null;

			if (_jump.IsActive)
			{
				yield break;
			}
		}

		if (IsActive)
		{
			_jump.SetEmpty();
		}
	}
}
