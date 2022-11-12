using UnityEngine;

[RequireComponent(typeof(WallChecker))]

public class PlayerWallJumpAS : AbilityState<PlayerJumpAbility>
{
	[SerializeField] private float _coyoteTime;
	[SerializeField] private float _jumpForse;
	[SerializeField] private Vector2 _angle;
	[SerializeField] private float _duration;

	private WallChecker _wallChecker;


	public float CoyoteTime
	{
		get => _coyoteTime;
	}

	protected override void Awake()
	{
		base.Awake();
		Checkers.Add(_wallChecker = GetComponent<WallChecker>());
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		Ability.RestoreJumps();
		Ability.DecreaseJumps();
		Ability.Player.BlockVelocity();
		Ability.Player.SetVelocity(_jumpForse, _angle, _wallChecker.WallDirection);
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		Ability.Player.UnlockVelocity();
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();

		if (ActiveTime > _duration || Mathf.Abs(Ability.Player.Velocity.x) < 0.01f)
		{
			if (Ability.Player.Velocity.y > 0f)
			{
				Ability.Player.SetVelocityY(Ability.Player.Velocity.y / 2);
			}

			Ability.OnExit();
		}
	}
}
