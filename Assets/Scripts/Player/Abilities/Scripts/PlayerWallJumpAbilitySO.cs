using UnityEngine;

[CreateAssetMenu(fileName = "PayerWallJumpAbility", menuName = "Player/Abilities/Wall Jump")]

public class PlayerWallJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private int _force;

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
			_jumpDirection = player.wallDirection;
		});

		useConditions.Add(() =>
		{
			return inputReader.jumpInput &&
						 !player.isClampedBetweenWalls &&
						 (player.isTouchingWall || player.isTouchingWallBack);
		});

		terminateConditions.Add(() =>
		{
			return _outOfWall &&
						 (Mathf.Abs(player.rb.velocity.x) <= 0.01f || player.rb.velocity.y < 0.01f);
		});

		useActions.Add(() =>
		{
			_outOfWall = false;
			player.HoldVelocity(_force, _angle, _jumpDirection);
			player.CheckIfShouldFlip(_jumpDirection);
			inputReader.jumpInput = false;
		});

		updateActions.Add(() =>
		{
			if (!player.isTouchingWall)
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
