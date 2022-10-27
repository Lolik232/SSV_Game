using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Player/Abilities/Jump")]

public class PlayerJumpAbilitySO : AbilitySO
{
	[SerializeField] private float _coyoteTime;

	[HideInInspector] protected new PlayerSO entity;

	private float _startCoyoteTime;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		enterConditions.Add(() =>
		{
			return entity.controller.jump &&
						 !entity.checkers.touchingCeiling;
		});

		exitConditions.Add(() =>
		{
			return entity.Velocity.y < 0f ||
						 !entity.controller.jumpInputHold;
		});

		enterActions.Add(() =>
		{
			entity.controller.jump = false;
			entity.TrySetVelocityY(entity.parameters.jumpForce);
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
