using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Player/Abilities/Jump")]

public class PlayerJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private float _coyoteTime;

	private float _startCoyoteTime;

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() =>
		{
			return data.input.jumpInput &&
						 !data.checkers.isTouchingCeiling;
		});

		terminateConditions.Add(() =>
		{
			return player.Velocity.y < 0f ||
						 !data.input.jumpInputHold;
		});

		useActions.Add(() =>
		{
			data.input.jumpInput = false;
			player.TrySetVelocityY(data.parameters.jumpForce);
		});

		terminateActions.Add(() =>
		{
			if (player.Velocity.y > 0f)
			{
				player.TrySetVelocityY(player.Velocity.y * 0.5f);
			}
		});
	}

	public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
