using System.Collections;

using UnityEngine;

[RequireComponent(typeof(CeilChecker), typeof(GroundChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker))]

public sealed class PlayerGroundedState : State
{
	[SerializeField] private float _waitForLedgeClimbTime;

	private Player _player;

	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;
	private CeilChecker _ceilChecker;

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
		_player = GetComponent<Player>();

		Checkers.Add(_groundChecker = GetComponent<GroundChecker>());
		Checkers.Add(_wallChecker = GetComponent<WallChecker>());
		Checkers.Add(_ledgeChecker = GetComponent<LedgeChecker>());
		Checkers.Add(_ceilChecker = GetComponent<CeilChecker>());
	}

	private void Start()
	{
		bool InAirCondition() => !_groundChecker.Grounded;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		_ledgeChecker.TouchingLegde &&
																		_player.IsStanding &&
																		_player.Input.Grab &&
																		_player.Input.Move.y != -1;

		bool OnLedgeCondition() => _ledgeClimbTimeOut;

		void InAirAction()
		{
			if (!_player.JumpAbility.IsActive)
			{
				StartCoroutine(_player.InAirState.CheckJumpCoyoteTime());
			}
		}

		Transitions.Add(new(_player.InAirState, InAirCondition, InAirAction));
		Transitions.Add(new(_player.TouchingWallState, TouchingWallCondition));
		Transitions.Add(new(_player.OnLedgeState, OnLedgeCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_player.MoveHorizontalAbility.Permited = true;
		_player.MoveVerticalAbility.Permited = false;
		_player.LedgeClimbAbility.Permited = false;
		_player.CrouchAbility.Permited = true;
		_player.JumpAbility.Permited = true;

		_player.JumpAbility.Restore();

		_tryingLedgeClimb = false;
		_ledgeClimbTimeOut = false;
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
					 (_player.Input.Move.x == _player.FacingDirection ||
						_player.Input.Move.y == 1);
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
