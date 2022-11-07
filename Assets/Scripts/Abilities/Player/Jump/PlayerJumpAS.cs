using UnityEngine;

[RequireComponent(typeof(Movable), typeof(JumpController))]

public class PlayerJumpAS : AbilityState<PlayerJumpAbility>
{
	[SerializeField] private float _coyoteTime;
	[SerializeField] private float _jumpForse;

	private Movable _movable;
	private JumpController _jumpController;

	public float CoyoteTime
	{
		get => _coyoteTime;
	}

	protected override void Awake()
	{
		base.Awake();
		_movable = GetComponent<Movable>();
		_jumpController = GetComponent<JumpController>();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_movable.SetVelocityY(_jumpForse);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();

		if (!_jumpController.Jump || _movable.Velocity.y < 0.01f)
		{
			if (_movable.Velocity.y > 0f)
			{
				_movable.SetVelocityY(_movable.Velocity.y / 2);
			}

			Ability.OnExit();
		}
	}
}
