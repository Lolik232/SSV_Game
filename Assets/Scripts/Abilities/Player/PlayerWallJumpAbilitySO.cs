using UnityEngine;

[CreateAssetMenu(fileName = "PayerWallJumpAbility", menuName = "Player/Abilities/Wall Jump")]

public class PlayerWallJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private float _coyoteTime;
	[SerializeField] private float _wallExitTime;

	[SerializeField] private Vector2 _angle;

	private bool _outOfWall;

	private float _startCoyoteTime;

	private int _jumpDirection;

	protected override void OnEnable()
	{
		base.OnEnable();

		beforeUseActions.Add(() =>
		{
			_jumpDirection = data.checkers.wallDirection;
		});

		useConditions.Add(() =>
		{
			return data.input.jumpInput &&
						 !data.checkers.isClampedBetweenWalls &&
						 !data.checkers.isTouchingCeiling &&
						 (data.checkers.isTouchingWall || data.checkers.isTouchingWallBack);
		});

		terminateConditions.Add(() =>
		{
			return _outOfWall &&
						 (Mathf.Abs(player.Velocity.x) <= 0.01f || player.Velocity.y < 0.01f);
		});

		useActions.Add(() =>
		{
			_outOfWall = false;
			player.HoldVelocity(data.parameters.wallJumpForce, _angle, _jumpDirection);
			player.CheckIfShouldFlip(_jumpDirection);
			data.input.jumpInput = false;
		});

		updateActions.Add(() =>
		{
			if (!data.checkers.isTouchingWall)
			{
				_outOfWall = true;
			}
		});

		terminateActions.Add(() =>
		{
			player.ReleaseVelocity();
		});
	}

	public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	public bool NeedHardExit() => Time.time > startTime + _wallExitTime;

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
