using UnityEngine;

public class PlayerJumpAS : AbilityState<PlayerJumpAbility>
{
	[SerializeField] private float _coyoteTime;
	[SerializeField] private float _jumpForse;

	public float CoyoteTime
	{
		get => _coyoteTime;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		if (Ability.AmountOfJumps == 0)
		{
			Ability.OnExit();
			return;
		}

		Ability.DecreaseJumps();
		Ability.Player.SetVelocityY(_jumpForse);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();

		if (!Ability.Player.Input.Jump || Ability.Player.Velocity.y < 0.01f)
		{
			if (Ability.Player.Velocity.y > 0f)
			{
				Ability.Player.SetVelocityY(Ability.Player.Velocity.y / 2);
			}

			Ability.OnExit();
		}
	}
}
