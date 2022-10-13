using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Player/Abilities/Jump")]

public class PlayerJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private int _force;
	[SerializeField] private float _coyoteTime;

	private float _startCoyoteTime;
	public bool CoyoteTime => Time.time < _startCoyoteTime + _coyoteTime;

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() =>
		{
			return Player.jumpInput && 
						 !Player.isTouchingCeiling;
		});

		terminateConditions.Add(() =>
		{
			return Player.Rb.velocity.y < 0f || 
						 !Player.jumpInputHold;
		});

		useActions.Add(() =>
		{
			Player.jumpInput = false;
			Player.TrySetVelocityY(_force);
		});

		terminateActions.Add(() =>
		{
			if (Player.Rb.velocity.y > 0f)
			{
				Player.TrySetVelocityY(Player.Rb.velocity.y * 0.5f);
			}
		});
	}

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
