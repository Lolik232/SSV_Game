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

		prepareActions.Add(() =>
		{
			_jumpDirection = data.checkers.wallDirection;
		});

		enterConditions.Add(() =>
		{
			return data.controller.jump &&
						 !data.checkers.clampedBetweenWalls &&
						 !data.checkers.touchingCeiling &&
						 (data.checkers.touchingWall || data.checkers.touchingWallBack);
		});

		exitConditions.Add(() =>
		{
			return _outOfWall &&
						 (Mathf.Abs(entity.Velocity.x) <= 0.01f || entity.Velocity.y < 0.01f);
		});

		enterActions.Add(() =>
		{
			_outOfWall = false;
			entity.HoldVelocity(data.parameters.wallJumpForce, _angle, _jumpDirection);
			entity.RotateIntoDirection(_jumpDirection);
			data.controller.jump = false;
		});

		updateActions.Add(() =>
		{
			if (!data.checkers.touchingWall)
			{
				_outOfWall = true;
			}
		});

		exitActions.Add(() =>
		{
			entity.ReleaseVelocity();
		});
	}

	public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	public bool NeedHardExit() => Time.time > startTime + _wallExitTime;

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
