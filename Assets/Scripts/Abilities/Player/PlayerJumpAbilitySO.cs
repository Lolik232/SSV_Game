using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Player/Abilities/Jump")]

public class PlayerJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private float _coyoteTime;

	private float _startCoyoteTime;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() =>
		{
			return data.controller.jump &&
						 !data.checkers.touchingCeiling;
		});

		exitConditions.Add(() =>
		{
			return entity.Velocity.y < 0f ||
						 !data.controller.jumpInputHold;
		});

		enterActions.Add(() =>
		{
			data.controller.jump = false;
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
