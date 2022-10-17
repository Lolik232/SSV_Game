using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : PlayerAbilitySO
{
	[SerializeField] private float _minProportion;
	[SerializeField] private float _dashGravity;

	private Vector2 _dashDirection;

	private int _gravityId;
	private int _velocityId;

	protected override void OnEnable()
	{
		base.OnEnable();

		beforeUseActions.Add(() =>
		{
			_dashDirection = inputReader.mouseInputDirection;
		});

		useConditions.Add(() =>
		{
			return inputReader.dashInput &&
						 _dashDirection != Vector2.zero &&
						 !(player.isTouchingCeiling && !player.isStanding);
		});

		terminateConditions.Add(() =>
		{
			return player.rb.velocity.magnitude <= parameters.dashForce * _minProportion ||
						 (!inputReader.dashInputHold && Time.time > startTime + duration * _minProportion);
		});

		useActions.Add(() =>
		{
			_gravityId = player.HoldGravity(_dashGravity);
			_velocityId = player.HoldVelocity(parameters.dashForce * _dashDirection);
			player.CheckIfShouldFlip(_dashDirection.x >= 0 ? 1 : -1);
			player.tr.emitting = true;
		});

		terminateActions.Add(() =>
		{
			inputReader.dashInput = false;
			player.ReleaseGravity(_gravityId);
			player.ReleaseVelocity(_velocityId);
			player.tr.emitting = false;
			player.tr.Clear();
			if (player.rb.velocity.y > 0f)
			{
				player.TrySetVelocityY(player.rb.velocity.y * 0.1f);
			}
		});
	}
}
