using UnityEngine;

[RequireComponent(typeof(Movable), typeof(Rotateable))]

public class MoveStopAbility : Ability, IMoveAbility
{
	[SerializeField] private AbilityParameter _deceleration;
	[SerializeField] private float _minMoveSpeed;

	private float _moveSpeed;

	protected Movable movable;
	protected Rotateable rotateable;

	private float Deceleration
	{
		get => _deceleration.value.Current;
	}

	public float MoveSpeed
	{
		get => _moveSpeed;
		set => _moveSpeed = Mathf.Max(_minMoveSpeed, value);
	}

	protected override void Awake()
	{
		base.Awake();
		movable = GetComponent<Movable>();
		rotateable = GetComponent<Rotateable>();

		_deceleration.value.Initialize();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		MoveSpeed = movable.Velocity.x;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();
		Decelerate();
	}

	private void Decelerate()
	{
		if (MoveSpeed > _minMoveSpeed)
		{
			if (_deceleration.required)
			{
				MoveSpeed -= Deceleration;
			}
			else
			{
				MoveSpeed = _minMoveSpeed;
			}
		}
	}
}
