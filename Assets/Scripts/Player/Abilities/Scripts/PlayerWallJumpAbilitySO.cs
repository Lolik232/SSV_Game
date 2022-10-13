using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PayerWallJumpAbility", menuName = "Player/Abilities/Wall Jump")]

public class PlayerWallJumpAbilitySO : PlayerAbilitySO
{
	[SerializeField] private int _force;
	[SerializeField] private Vector2 _angle;
	[SerializeField] private float _coyoteTime;

	[NonSerialized] public bool outOfWall;

	private float _startCoyoteTime;
	public bool CoyoteTime => Time.time < _startCoyoteTime + _coyoteTime;

	[SerializeField] private float _wallExitTime;
	public bool NeedHardExit() => Time.time > startTime + _wallExitTime;

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() =>
		{
			return Player.jumpInput &&
						 !Player.isClampedBetweenWalls &&
						 (Player.isTouchingWall || Player.isTouchingWallBack);
		});

		terminateConditions.Add(() =>
		{
			return Mathf.Abs(Player.Rb.velocity.x) <= 0.01f && 
						 outOfWall;
		});

		useActions.Add(() =>
		{
			outOfWall = false;
			Player.HoldVelocity(_force, _angle, Player.wallDirection);
			Player.CheckIfShouldFlip(Player.wallDirection);
			Player.jumpInput = false;
		});

		updateActions.Add(() =>
		{
			if (!Player.isTouchingWall)
			{
				outOfWall = true;
			}
		});

		terminateActions.Add(() =>
		{
			Player.ReleaseVelocity();
		});
	}

	public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
