using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Player/Abilities/Jump")]

public class PlayerJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private int _force;

	[SerializeField] private float _coyoteTime;

	private float _startCoyoteTime;

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() =>
		{
			return inputReader.jumpInput &&
						 !player.isTouchingCeiling;
		});

		terminateConditions.Add(() =>
		{
			return player.rb.velocity.y < 0f ||
						 !inputReader.jumpInputHold;
		});

		useActions.Add(() =>
		{
			inputReader.jumpInput = false;
			player.TrySetVelocityY(_force);
		});

		terminateActions.Add(() =>
		{
			if (player.rb.velocity.y > 0f)
			{
				player.TrySetVelocityY(player.rb.velocity.y * 0.5f);
			}
		});
	}

	public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
