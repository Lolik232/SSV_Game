using UnityEngine;

[RequireComponent(typeof(Movable), typeof(Rotateable))]

public abstract class MoveAbility : Ability, IMoveAbility
{
	[SerializeField] private AbilityParameter _acceleration;
	[SerializeField] private float _maxMoveSpeed;

	private float _moveSpeed;

	protected Movable movable;
	protected Rotateable rotateable;

	private float Acceleration
	{
		get => _acceleration.value.Current;
	}

	public float MoveSpeed
	{
		get => _moveSpeed;
		set => _moveSpeed = Mathf.Min(_maxMoveSpeed, value);
	}

	protected override void Awake()
	{
		base.Awake();
		movable = GetComponent<Movable>();
		rotateable = GetComponent<Rotateable>();

		_acceleration.value.Initialize();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		MoveSpeed = movable.Velocity.x;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Accelerate();
	}

	private void Accelerate()
	{
		if (MoveSpeed < _maxMoveSpeed)
		{
			if (_acceleration.required)
			{
				MoveSpeed += Acceleration;
			}
			else
			{
				MoveSpeed = _maxMoveSpeed;
			}
		}
	}
}
