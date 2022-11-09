using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]

public class PlayerDashAS : AbilityState<PlayerDashAbility>
{
	[SerializeField] private float _duration;
	[SerializeField] private float _dashForse;

	private TrailRenderer _tr;

	protected override void Awake()
	{
		base.Awake();
		_tr = GetComponent<TrailRenderer>();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		Vector2 dashDirection = (Ability.Player.Input.LookAt - Ability.Player.Center).normalized;
		Ability.Player.SetVelocity(_dashForse * dashDirection);
		Ability.Player.RotateIntoDirection(dashDirection.x > 0 ? 1 : -1);
		Ability.Player.BlockVelocity();

		_tr.Clear();
		_tr.emitting = true;
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		Ability.Player.UnlockVelocity();

		_tr.emitting = false;
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();

		if (ActiveTime > _duration || Ability.Player.Velocity.magnitude < _dashForse / 2)
		{
			if (Ability.Player.Velocity.y > 0f)
			{
				Ability.Player.SetVelocityY(Ability.Player.Velocity.y / 5);
			}

			Ability.OnExit();
		}
	}
}
