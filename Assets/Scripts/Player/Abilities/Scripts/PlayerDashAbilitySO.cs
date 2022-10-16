using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : PlayerAbilitySO
{
	[SerializeField] private float _minProportion;

	private float _cachedGravity;
	private Vector2 _dashDirection;

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
						 (!inputReader.dashInput && Time.time > startTime + duration * _minProportion);
		});

		useActions.Add(() =>
		{
			_cachedGravity = player.rb.gravityScale;
			player.rb.gravityScale = 0f;
			player.CheckIfShouldFlip(_dashDirection.x >= 0 ? 1 : -1);
			player.HoldVelocity(parameters.dashForce * _dashDirection);
			player.tr.emitting = true;
		});

		terminateActions.Add(() =>
		{
			inputReader.dashInput = false;
			player.rb.gravityScale = _cachedGravity;
			player.ReleaseVelocity();
			player.tr.emitting = false;
			player.tr.Clear();
			if (player.rb.velocity.y > 0f)
			{
				player.TrySetVelocityY(player.rb.velocity.y * 0.1f);
			}
		});
	}
}
