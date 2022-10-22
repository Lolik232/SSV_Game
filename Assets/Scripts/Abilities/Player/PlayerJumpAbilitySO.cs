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
						 !data.checkers.touchingCeiling;
		});

		terminateConditions.Add(() =>
		{
			return entity.Velocity.y < 0f ||
						 !data.input.jumpInputHold;
		});

		enterActions.Add(() =>
		{
			data.input.jumpInput = false;
			entity.TrySetVelocityY(data.parameters.jumpForce);
		});

		exitActions.Add(() =>
		{
			if (entity.Velocity.y > 0f)
			{
				entity.TrySetVelocityY(entity.Velocity.y * 0.5f);
			}
		});
	}

	public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
