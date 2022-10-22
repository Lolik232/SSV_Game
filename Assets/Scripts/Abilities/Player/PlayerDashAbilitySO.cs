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

		beforeEnterActions.Add(() =>
		{
			_dashDirection = data.input.mouseInputDirection;
		});

		useConditions.Add(() =>
		{
			return data.input.dashInput &&
						 _dashDirection != Vector2.zero &&
						 !(data.checkers.touchingCeiling && !entity.isStanding);
		});

		terminateConditions.Add(() =>
		{
			return entity.Velocity.magnitude <= data.parameters.dashForce * _minProportion ||
						 (!data.input.dashInputHold && Time.time > startTime + duration * _minProportion);
		});

		enterActions.Add(() =>
		{
			entity.HoldGravity(_dashGravity);
			entity.HoldVelocity(data.parameters.dashForce * _dashDirection);
			entity.CheckIfShouldFlip(_dashDirection.x >= 0 ? 1 : -1);
			data.input.dashInput = false;
			entity.EnableTrail();
		});

		exitActions.Add(() =>
		{
			entity.ReleaseGravity();
			entity.ReleaseVelocity();
			entity.DisableTrail();
			if (entity.Velocity.y > 0f)
			{
				entity.TrySetVelocityY(entity.Velocity.y * 0.1f);
			}
		});
	}
}
