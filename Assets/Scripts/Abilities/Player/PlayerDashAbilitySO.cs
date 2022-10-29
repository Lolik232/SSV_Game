using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : AbilitySO
{
	[SerializeField] private float _minProportion;
	[SerializeField] private float _dashGravity;

	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	private Vector2 _dashDirection;

	protected override void OnEnable()
	{
		entity = base.entity as PlayerSO;

		base.OnEnable();

		prepareActions.Add(() =>
		{
			_dashDirection = entity.controller.lookAtDirection;
		});

		enterConditions.Add(() =>
		{
			return entity.controller.dash &&
						 _dashDirection != Vector2.zero &&
						 !(entity.checkers.touchingCeiling && !entity.isStanding);
		});

		exitConditions.Add(() =>
		{
			return entity.Velocity.magnitude <= entity.parameters.dashForce * _minProportion ||
						 (!entity.controller.dashInputHold && Time.time > startTime + duration * _minProportion);
		});

		enterActions.Add(() =>
		{
			entity.HoldGravity(_dashGravity);
			entity.HoldVelocity(entity.parameters.dashForce * _dashDirection);
			entity.RotateIntoDirection(Mathf.RoundToInt(_dashDirection.x));
			entity.controller.dash = false;
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
