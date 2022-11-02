using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Condition : IComponent, ICondition
{
	[SerializeField] private string _description;
	[SerializeField] private List<ExpectedBoolValue> _bools;
	[SerializeField] private List<ExpectedVector2IntValue> _vectors;
	[SerializeField] private List<ExpectedFloatValue> _floats;

	private GroundChecker _groundChecker;
	private CeilChecker _ceilChecker;
	private WallChecker _wallChecker;
	private LedgeChecker _ledgeChecker;
	private MoveController _moveController;
	private JumpController _jumpController;
	private DashController _dashController;
	private GrabController _grabController;
	private AttackController _attackController;
	private AbilityController _abilityController;
	private Movable _movable;
	private Rotateable _rotateable;

	public bool DoChecks()
	{
		foreach (var el in _bools)
		{
			if (!Check(el))
			{
				return false;
			}
		}

		foreach (var el in _vectors)
		{
			if (!Check(el))
			{
				return false;
			}
		}

		foreach (var el in _floats)
		{
			if (!Check(el))
			{
				return false;
			}
		}

		return true;
	}

	public void Initialize(GameObject origin)
	{
		_groundChecker = origin.GetComponent<GroundChecker>();
		_ceilChecker = origin.GetComponent<CeilChecker>();
		_wallChecker = origin.GetComponent<WallChecker>();
		_ledgeChecker = origin.GetComponent<LedgeChecker>();
		_moveController = origin.GetComponent<MoveController>();
		_jumpController = origin.GetComponent<JumpController>();
		_dashController = origin.GetComponent<DashController>();
		_grabController = origin.GetComponent<GrabController>();
		_attackController = origin.GetComponent<AttackController>();
		_abilityController = origin.GetComponent<AbilityController>();
		_movable = origin.GetComponent<Movable>();
		_rotateable = origin.GetComponent<Rotateable>();
	}
	private bool Check(ExpectedBoolValue expected)
	{
		switch (expected.type)
		{
			case ExpectedBoolValueType.Grounded:
				return Check(expected.value, _groundChecker.Grounded);
			case ExpectedBoolValueType.TouchingWall:
				return Check(expected.value, _wallChecker.TouchingWall);
			case ExpectedBoolValueType.TouchingWallBack:
				return Check(expected.value, _wallChecker.TouchingWallBack);
			case ExpectedBoolValueType.TouchingCeiling:
				return Check(expected.value, _ceilChecker.TouchingCeiling);
			case ExpectedBoolValueType.TouchingLedge:
				return Check(expected.value, _ledgeChecker.TouchingLegde);

			case ExpectedBoolValueType.Jump:
				return Check(expected.value, _jumpController.Jump);
			case ExpectedBoolValueType.Dash:
				return Check(expected.value, _dashController.Dash);
			case ExpectedBoolValueType.Grab:
				return Check(expected.value, _grabController.Grab);
			case ExpectedBoolValueType.Attack:
				return Check(expected.value, _attackController.Attack);
			case ExpectedBoolValueType.Ability:
				return Check(expected.value, _abilityController.Ability);

			default:
				Debug.Log("Type \"" + expected.type.ToString() + "\" doesn`t exist");
				return false;
		}
	}

	private bool Check(ExpectedVector2IntValue expected)
	{
		switch (expected.type)
		{
			case ExpectedVector2IntValueType.Move:
				return Check(expected.value, _moveController.Move);

			default:
				Debug.Log("Type \"" + expected.type.ToString() + "\" doesn`t exist");
				return false;
		}
	}

	private bool Check(ExpectedFloatValue expected)
	{
		switch (expected.type)
		{
			case ExpectedFloatValueType.VelocityX:
				return Check(expected.value, _movable.Velocity.x);
			case ExpectedFloatValueType.VelocityY:
				return Check(expected.value, _movable.Velocity.y);

			default:
				Debug.Log("Type \"" + expected.type.ToString() + "\" doesn`t exist");
				return false;
		}
	}

	private bool Check(BoolValue expected, bool got)
	{
		return got && expected == BoolValue.True ||
					 !got && expected == BoolValue.False;
	}

	private bool Check(Vector2IntValue expected, Vector2Int got)
	{
		return expected == Vector2IntValue.Zero && got == Vector2Int.zero ||
					 expected == Vector2IntValue.Up && got.y == 1 ||
					 expected == Vector2IntValue.Down && got.y == -1 ||
					 expected == Vector2IntValue.Forward && got.x == _rotateable.FacingDirection ||
					 expected == Vector2IntValue.Backward && got.x == -_rotateable.FacingDirection ||
					 expected == Vector2IntValue.NotZero && got != Vector2Int.zero ||
					 expected == Vector2IntValue.NotUp && got.y != 1 ||
					 expected == Vector2IntValue.NotDown && got.y != -1 ||
					 expected == Vector2IntValue.NotForward && got.x != _rotateable.FacingDirection ||
					 expected == Vector2IntValue.NotBackward && got.x != -_rotateable.FacingDirection ||
					 expected == Vector2IntValue.Horizontal && got.x != 0 ||
					 expected == Vector2IntValue.Vertical && got.y != 0 ||
					 expected == Vector2IntValue.NotHorizontal && got.x == 0 ||
					 expected == Vector2IntValue.NotVertical && got.y == 0;
	}

	private bool Check(FloatValue expected, float got)
	{
		return expected == ComparedValue.Equal && got == expected.to ||
					 expected == ComparedValue.NotEqual && got != expected.to ||
					 expected == ComparedValue.Greater && got > expected.to ||
					 expected == ComparedValue.NotLower && got >= expected.to ||
					 expected == ComparedValue.Lower && got < expected.to ||
					 expected == ComparedValue.NotGreater && got <= expected.to;
	}

	private enum ExpectedBoolValueType
	{
		Grounded,
		TouchingCeiling,
		TouchingWall,
		TouchingWallBack,
		TouchingLedge,

		Jump,
		Dash,
		Grab,
		Attack,
		Ability
	}

	private enum ExpectedFloatValueType
	{
		VelocityX,
		VelocityY,
	}

	private enum ExpectedVector2IntValueType
	{
		Move
	}

	private enum BoolValue
	{
		True, False
	}

	private enum Vector2IntValue
	{
		Zero, Forward, Backward, Up, Down,
		NotZero, NotForward, NotBackward, NotUp, NotDown,
		Horizontal, Vertical, NotHorizontal, NotVertical
	}

	private enum ComparedValue
	{
		Greater, NotGreater, Lower, NotLower, Equal, NotEqual
	}

	[Serializable]
	private struct ExpectedBoolValue
	{
		[SerializeField] private string _description;
		public ExpectedBoolValueType type;
		public BoolValue value;
	}

	[Serializable]
	private struct ExpectedVector2IntValue
	{
		[SerializeField] private string _description;
		public ExpectedVector2IntValueType type;
		public Vector2IntValue value;
	}

	[Serializable]
	private struct ExpectedFloatValue
	{
		[SerializeField] private string _description;
		public ExpectedFloatValueType type;
		public FloatValue value;
	}

	[Serializable]
	private struct FloatValue
	{
		public ComparedValue value;
		public float to;

		public static implicit operator ComparedValue(FloatValue floatValue) => floatValue.value;
	}
}
