using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PayerWallJumpAbility", menuName = "Player/Abilities/Wall Jump")]

public class PlayerWallJumpAbilitySO : AbilitySO
{
	//[SerializeField] private float _coyoteTime;
	//[SerializeField] private float _wallExitTime;

	//[SerializeField] private Vector2 _angle;

	//private bool _outOfWall;

	//private float _startCoyoteTime;

	//private int _jumpDirection;

	//protected Movable movable;

	//private int _velocityHolder;

	//protected override void OnEnable()
	//{
	//	base.OnEnable();

	//	prepareActions.Add(() =>
	//	{
	//		_jumpDirection = entity.checkers.wallDirection;
	//	});

	//	enterConditions.Add(() =>
	//	{
	//		return entity.controller.jump &&
	//					 !entity.checkers.clampedBetweenWalls &&
	//					 !entity.checkers.touchingCeiling &&
	//					 (entity.checkers.touchingWall || entity.checkers.touchingWallBack);
	//	});

	//	exitConditions.Add(() =>
	//	{
	//		return _outOfWall &&
	//					 (Mathf.Abs(movable.Velocity.x) <= 0.01f || movable.Velocity.y < 0.01f);
	//	});

	//	enterActions.Add(() =>
	//	{
	//		_outOfWall = false;
	//		_velocityHolder = movable.HoldVelocity(entity.parameters.wallJumpForce, _angle, _jumpDirection);
	//		movable.TryRotateIntoDirection(_jumpDirection);
	//		entity.controller.jump = false;
	//	});

	//	updateActions.Add(() =>
	//	{
	//		if (!entity.checkers.touchingWall)
	//		{
	//			_outOfWall = true;
	//		}
	//	});

	//	exitActions.Add(() =>
	//	{
	//		movable.ReleaseVelocity(_velocityHolder);
	//	});
	//}

	//public override void Initialize(GameObject origin)
	//{
	//	base.Initialize(origin);
	//	movable = origin.GetComponent<Movable>();
	//}

	//public bool IsCoyoteTime() => Time.time < _startCoyoteTime + _coyoteTime;

	//public bool NeedHardExit() => Time.time > startTime + _wallExitTime;

	//public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
