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

		prepareActions.Add(() =>
		{
			_dashDirection = data.controller.lookAtDirection;
		});

		enterConditions.Add(() =>
		{
			return data.controller.dash &&
						 _dashDirection != Vector2.zero &&
						 !(data.checkers.touchingCeiling && !entity.isStanding);
		});

		exitConditions.Add(() =>
		{
			return entity.Velocity.magnitude <= data.parameters.dashForce * _minProportion ||
						 (!data.controller.dashInputHold && Time.time > startTime + duration * _minProportion);
		});

		enterActions.Add(() =>
		{
			entity.HoldGravity(_dashGravity);
			entity.HoldVelocity(data.parameters.dashForce * _dashDirection);
			entity.RotateIntoDirection(Mathf.RoundToInt(_dashDirection.x));
			data.controller.dash = false;
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
