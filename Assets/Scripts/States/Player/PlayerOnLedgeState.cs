using UnityEngine;

[RequireComponent(typeof(WallChecker), typeof(LedgeChecker), typeof(Physical))]
[RequireComponent(typeof(Movable))]

public class PlayerOnLedgeState : State
{
	private PlayerGroundedState _grounded;

	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;

	private Physical _physical;
	private Movable _movable;

	private PlayerLedgeClimbAbility _ledgeClimb;
	private PlayerCrouchAbility _crouch;

	private Vector2 _startPosition;
	private Vector2 _endPosition;

	protected override void Awake()
	{
		base.Awake();
		_grounded = GetComponent<PlayerGroundedState>();

		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();

		_physical = GetComponent<Physical>();
		_movable = GetComponent<Movable>();

		_ledgeClimb = GetComponent<PlayerLedgeClimbAbility>();
		_crouch = GetComponent<PlayerCrouchAbility>();

		bool GroundedCondition() => !_ledgeClimb.IsActive;

		void GroundedAction()
		{
			//_crouch.OnEnter();
		}

		Transitions.Add(new(_grounded, GroundedCondition, GroundedAction));
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_startPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * (_physical.Size.x / 2 + IChecker.CHECK_OFFSET),
																 _ledgeChecker.GroundPosition.y - 1f);
		_endPosition = new Vector2(_wallChecker.WallPosition.x - _wallChecker.WallDirection * (_physical.Size.x / 2 + IChecker.CHECK_OFFSET),
																 _ledgeChecker.GroundPosition.y + IChecker.CHECK_OFFSET);
		_movable.SetPosition(_startPosition);
		_movable.SetVelocity(Vector2.zero);
		_movable.SetGravity(0f);

		_ledgeClimb.Unlock();
		_ledgeClimb.OnEnter();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_movable.SetPosition(_endPosition);
		_movable.ResetGravity();

		_ledgeClimb.Block();
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(_startPosition + _physical.Offset, _physical.Size);
		Gizmos.DrawWireCube(_endPosition + _physical.Offset, _physical.Size);
	}
}
