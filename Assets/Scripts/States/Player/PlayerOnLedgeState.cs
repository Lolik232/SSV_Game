using UnityEngine;

[RequireComponent(typeof(WallChecker), typeof(LedgeChecker))]

public class PlayerOnLedgeState : State
{
	private Player _player;

	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private Vector2 _startPosition;
	private Vector2 _endPosition;

	protected override void Awake()
	{
		base.Awake();
		_player = GetComponent<Player>();

		Checkers.Add(_wallChecker = GetComponent<WallChecker>());
		Checkers.Add(_ledgeChecker = GetComponent<LedgeChecker>());
	}

	private void Start()
	{
		bool GroundedCondition() => !_player.LedgeClimbAbility.IsActive;

		Transitions.Add(new(_player.GroundedState, GroundedCondition));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_player.MoveHorizontalAbility.Permited = false;
		_player.MoveVerticalAbility.Permited = false;
		_player.LedgeClimbAbility.Permited = true;
		_player.CrouchAbility.Permited = false;
		_player.JumpAbility.Permited = false;
		_player.DashAbility.Permited = false;
		_player.AttackAbility.Permited = false;

		_player.SetPosition(_startPosition);
		_player.SetVelocity(Vector2.zero);
		_player.SetGravity(0f);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_player.SetPosition(_endPosition);
		_player.ResetGravity();

		_player.CrouchAbility.Request(_player.CrouchAbility.Crouch);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(_startPosition + _player.Offset, _player.Size);
		Gizmos.DrawWireCube(_endPosition + _player.Offset, _player.Size);
	}

	public void DetermineLedgePosition()
	{
		_startPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * (_player.Size.x / 2 + IChecker.CHECK_OFFSET),
																	 _ledgeChecker.GroundPosition.y - 1f);
		_endPosition = new Vector2(_wallChecker.WallPosition.x - _wallChecker.WallDirection * (_player.Size.x / 2 + IChecker.CHECK_OFFSET),
																 _ledgeChecker.GroundPosition.y + IChecker.CHECK_OFFSET);
	}
}
