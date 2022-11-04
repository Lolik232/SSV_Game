using UnityEngine;

[RequireComponent(typeof(Movable), typeof(Physical))]
[RequireComponent(typeof(WallChecker), typeof(LedgeChecker))]

public class PlayerLedgeClimbAbility : Ability
{
	[SerializeField] private State _ledgeEndState;

	private Vector2 _startPosition;
	private Vector2 _endPosition;

	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;
	private Physical _physical;
	private Movable _movable;
	private StateMachine _stateMachine;

	protected override void Awake()
	{
		base.Awake();
		_wallChecker = GetComponent<WallChecker>();
		_ledgeChecker = GetComponent<LedgeChecker>();
		_physical = GetComponent<Physical>();
		_movable = GetComponent<Movable>();
		_stateMachine = GetComponent<StateMachine>();
	}

	protected override void ApplyPrepareActions()
	{
		base.ApplyPrepareActions();
		_startPosition = new Vector2(_wallChecker.WallPosition.x + _wallChecker.WallDirection * (_physical.Size.x / 2 + 0.01f), 
																 _ledgeChecker.GroundPosition.y - _ledgeChecker.YOffset + 0.01f);
		_endPosition = new Vector2(_wallChecker.WallPosition.x - _wallChecker.WallDirection * (_physical.Size.x / 2 + 0.01f),
																 _ledgeChecker.GroundPosition.y + 0.01f);
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_movable.SetPosition(_startPosition);
		_movable.SetGravity(0f);
		_movable.SetVelocity(Vector2.zero);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_movable.SetPosition(_endPosition);
		_movable.ResetGravity();
	}
}
