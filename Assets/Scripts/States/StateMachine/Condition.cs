using System;

using UnityEngine;

[Serializable]
public struct Condition : IComponent, ICondition
{
	[SerializeField] private BoolValue _expectedGrounded;
	[SerializeField] private BoolValue _expectedTouchingCeiling;
	[SerializeField] private BoolValue _expectedTouchingWall;
	[SerializeField] private BoolValue _expectedTouchingWallBack;
	[Space]
	[SerializeField] private Vector2IntValue _expectedMove;
	[SerializeField] private BoolValue _expectedJump;
	[SerializeField] private BoolValue _expectedDash;
	[SerializeField] private BoolValue _expectedGrab;
	[SerializeField] private BoolValue _expectedAttack;
	[SerializeField] private BoolValue _expectedAbility;
	[Space]
	[SerializeField] private FloatValue _expectedVelocityX;
	[SerializeField] private FloatValue _expectedVelocityY;

	private GroundChecker _groundChecker;
	private CeilChecker _ceilChecker;
	private WallChecker _wallChecker;
	private MoveController _moveController;
	private JumpController _jumpController;
	private DashController _dashController;
	private GrabController _grabController;
	private AttackController _attackController;
	private AbilityController _abilityController;
	private Movable _movable;

	public bool DoChecks()
	{
		return CheckBool(_expectedGrounded, _groundChecker.Grounded) &&
					 CheckBool(_expectedTouchingCeiling, _ceilChecker.TouchingCeiling) &&
					 CheckBool(_expectedTouchingWall, _wallChecker.TouchingWall) &&
					 CheckBool(_expectedTouchingWallBack, _wallChecker.TouchingWallBack) &&

					 CheckVector2Int(_expectedMove, _moveController.Move) &&
					 CheckBool(_expectedJump, _jumpController.Jump) &&
					 CheckBool(_expectedDash, _dashController.Dash) &&
					 CheckBool(_expectedGrab, _grabController.Grab) &&
					 CheckBool(_expectedAttack, _attackController.Attack) &&
					 CheckBool(_expectedAbility, _abilityController.Ability) &&

					 CheckFloat(_expectedVelocityX, _movable.Velocity.x) &&
					 CheckFloat(_expectedVelocityY, _movable.Velocity.y);
	}

	public void Initialize(GameObject origin)
	{
		_movable = origin.GetComponent<Movable>();
		_groundChecker = origin.GetComponent<GroundChecker>();
		_ceilChecker = origin.GetComponent<CeilChecker>();
		_wallChecker = origin.GetComponent<WallChecker>();
		_moveController = origin.GetComponent<MoveController>();
		_jumpController = origin.GetComponent<JumpController>();
		_dashController = origin.GetComponent<DashController>();
		_grabController = origin.GetComponent<GrabController>();
		_attackController = origin.GetComponent<AttackController>();
		_abilityController = origin.GetComponent<AbilityController>();
	}

	private bool CheckBool(BoolValue expected, bool got)
	{
		return expected == BoolValue.None ||
					 got && expected == BoolValue.True ||
					 !got && expected == BoolValue.False;
	}

	private bool CheckVector2Int(Vector2IntValue expected, Vector2Int got)
	{
		return expected == Vector2IntValue.None ||
					 expected == Vector2IntValue.Zero && got == Vector2Int.zero ||
					 expected == Vector2IntValue.Up && got.y == 1 ||
					 expected == Vector2IntValue.Down && got.y == -1 ||
					 expected == Vector2IntValue.Forward && got.x == _movable.FacingDirection ||
					 expected == Vector2IntValue.Backward && got.x == -_movable.FacingDirection;
	}

	private bool CheckFloat(FloatValue expected, float got)
	{
		return expected == ComparedValue.None ||
					 expected == ComparedValue.Equal && got == expected.to ||
					 expected == ComparedValue.NotEqual && got != expected.to ||
					 expected == ComparedValue.Greater && got > expected.to ||
					 expected == ComparedValue.NotLower && got >= expected.to ||
					 expected == ComparedValue.Lower && got < expected.to ||
					 expected == ComparedValue.NotGreater && got <= expected.to;
	}

	private enum BoolValue
	{
		None, True, False
	}

	private enum Vector2IntValue
	{
		None, Zero, Forward, Backward, Up, Down
	}

	private enum ComparedValue
	{
		None, Greater, NotGreater, Lower, NotLower, Equal, NotEqual
	}

	[Serializable]
	private struct FloatValue
	{
		public ComparedValue value;
		public float to;

		public FloatValue(ComparedValue value, float to)
		{
			this.value = value;
			this.to = to;
		}

		public static implicit operator ComparedValue(FloatValue floatValue) => floatValue.value;
	}
}
