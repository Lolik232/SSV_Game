using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : PlayerAbilitySO
{
	private float _cachedGravity;

	[SerializeField] private int _force;

	[SerializeField] private float _minProportion;

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() =>
		{
			return Player.dashInput && 
						 Player.dashDirection != Vector2.zero && 
						 !Player.isTouchingCeiling;
		});

		terminateConditions.Add(() =>
		{
			return Player.Rb.velocity.magnitude <= _force * _minProportion;
		});

		useActions.Add(() =>
		{
			_cachedGravity = Player.Rb.gravityScale;
			Player.Rb.gravityScale = 0f;
			Player.CheckIfShouldFlip(Player.dashDirection.x >= 0 ? 1 : -1);
			Player.HoldVelocity(_force * Player.dashDirection);
			Player.Tr.emitting = true;
			Player.dashInput = false;
		});

		terminateActions.Add(() =>
		{
			Player.Rb.gravityScale = _cachedGravity;
			Player.ReleaseVelocity();
			Player.Tr.emitting = false;
			if (Player.Rb.velocity.y > 0f)
			{
				Player.TrySetVelocityY(Player.Rb.velocity.y * 0.1f);
			}
		});
	}
}
