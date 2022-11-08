using System.Collections;

using UnityEngine;

[RequireComponent(typeof(GroundChecker), typeof(WallChecker), typeof(LedgeChecker))]

public sealed class PlayerInAirState : State
{
	private Player _player;

	private GroundChecker _groundChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	protected override void Awake()
	{
		base.Awake();
		_player = GetComponent<Player>();

		Checkers.Add(_groundChecker = GetComponent<GroundChecker>());
		Checkers.Add(_wallChecker = GetComponent<WallChecker>());
		Checkers.Add(_ledgeChecker = GetComponent<LedgeChecker>());
	}

	private void Start()
	{
		bool GroundedCondition() => _groundChecker.Grounded && _player.Velocity.y < 0.01f;

		bool TouchingWallCondition() => _wallChecker.TouchingWall &&
																		 _ledgeChecker.TouchingLegde &&
																		 (_player.Input.Grab || _player.Input.Move.x == _player.FacingDirection);

		bool OnLedgeCondition() => _wallChecker.TouchingWall && !_ledgeChecker.TouchingLegde && _player.Input.Move.y != -1;

		Transitions.Add(new(_player.GroundedState, GroundedCondition));
		Transitions.Add(new(_player.TouchingWallState, TouchingWallCondition));
		Transitions.Add(new(_player.OnLedgeState, OnLedgeCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_player.MoveHorizontalAbility.Permited = true;
		_player.MoveVerticalAbility.Permited = false;
		_player.LedgeClimbAbility.Permited = false;
		_player.CrouchAbility.Permited = false;
		_player.JumpAbility.Permited = true;
	}

	public IEnumerator CheckJumpCoyoteTime()
	{
		yield return new WaitUntil(() => IsActive);

		while (IsActive && ActiveTime < _player.JumpAbility.NormalJump.CoyoteTime)
		{
			yield return null;

			if (_player.JumpAbility.IsActive)
			{
				yield break;
			}
		}

		if (IsActive)
		{
			_player.JumpAbility.DecreaseJumps();
		}
	}
}
