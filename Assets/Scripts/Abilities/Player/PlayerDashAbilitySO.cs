using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : PlayerAbilitySO
{
	[SerializeField] private float _minProportion;
	[SerializeField] private float _dashGravity;

	private Vector2 _dashDirection;

	protected override void OnEnable()
	{
		base.OnEnable();

		beforeUseActions.Add(() =>
		{
			_dashDirection = data.input.mouseInputDirection;
		});

		useConditions.Add(() =>
		{
			return data.input.dashInput &&
						 _dashDirection != Vector2.zero &&
						 !(data.checkers.isTouchingCeiling && !player.isStanding);
		});

		terminateConditions.Add(() =>
		{
			return player.Velocity.magnitude <= data.parameters.dashForce * _minProportion ||
						 (!data.input.dashInputHold && Time.time > startTime + duration * _minProportion);
		});

		useActions.Add(() =>
		{
			player.HoldGravity(_dashGravity);
			player.HoldVelocity(data.parameters.dashForce * _dashDirection);
			player.CheckIfShouldFlip(_dashDirection.x >= 0 ? 1 : -1);
			data.input.dashInput = false;
			player.EnableTrail();
		});

		terminateActions.Add(() =>
		{
			player.ReleaseGravity();
			player.ReleaseVelocity();
			player.DisableTrail();
			if (player.Velocity.y > 0f)
			{
				player.TrySetVelocityY(player.Velocity.y * 0.1f);
			}
		});
	}
}
