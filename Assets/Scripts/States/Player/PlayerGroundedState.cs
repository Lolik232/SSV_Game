using System;
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

		void InAirAction()
		{
			if (!_player.IsStanding && _ceilChecker.TouchingCeiling)
			{
				_player.SetPositionY(_player.Position.y - (_player.StandSize.y - _player.CrouchSize.y));
			}

			if (!_player.JumpAbility.IsActive)
			{
				_player.InAirState.CheckJumpCoyoteTime();
			}
		}

		Transitions.Add(new(_player.InAirState, InAirCondition, InAirAction));
		Transitions.Add(new(_player.TouchingWallState, TouchingWallCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_player.MoveHorizontalAbility.Permited = true;
		_player.MoveVerticalAbility.Permited = false;
		_player.LedgeClimbAbility.Permited = false;
		_player.CrouchAbility.Permited = true;
		_player.JumpAbility.Permited = true;
		_player.DashAbility.Permited = true;

		_player.JumpAbility.RestoreJumps();
		_player.JumpAbility.CancelRequest();
		_player.DashAbility.RestoreDashes();

		_player.InAirState.TerminateTryingLedgeClimb();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		_player.InAirState.TryLedgeClimb();
	}
}
