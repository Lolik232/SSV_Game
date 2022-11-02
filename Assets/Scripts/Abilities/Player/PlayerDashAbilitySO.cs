using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : Ability
{
	//[SerializeField] private float _minProportion;
	//[SerializeField] private float _dashGravity;

	//private Vector2 _dashDirection;

	//protected Movable movable;
	//protected Crouchable crouchable;

	//private int _gravityHolder;
	//private int _velocityHolder;

	//protected override void OnEnable()
	//{
	//	base.OnEnable();

	//	prepareActions.Add(() =>
	//	{
	//		_dashDirection = entity.controller.lookAtDirection;
	//	});

	//	enterConditions.Add(() =>
	//	{
	//		return entity.controller.dash &&
	//					 _dashDirection != Vector2.zero &&
	//					 !(entity.checkers.touchingCeiling && !crouchable.IsStanding);
	//	});

	//	exitConditions.Add(() =>
	//	{
	//		return movable.Velocity.magnitude <= entity.parameters.dashForce * _minProportion ||
	//					 (!entity.controller.dashInputHold && Time.time > startTime + duration * _minProportion);
	//	});

	//	enterActions.Add(() =>
	//	{
	//		_gravityHolder = movable.HoldGravity(_dashGravity);
	//		_velocityHolder = movable.HoldVelocity(entity.parameters.dashForce * _dashDirection);
	//		movable.TryRotateIntoDirection(Mathf.RoundToInt(_dashDirection.x));
	//		entity.controller.dash = false;
	//		//movable.EnableTrail();
	//	});

	//	exitActions.Add(() =>
	//	{
	//		movable.ReleaseGravity(_gravityHolder);
	//		movable.ReleaseVelocity(_velocityHolder);
	//		//movable.DisableTrail();
	//		if (movable.Velocity.y > 0f)
	//		{
	//			movable.TrySetVelocityY(movable.Velocity.y * 0.1f);
	//		}
	//	});
	//}
	//public override void Initialize(GameObject origin)
	//{
	//	base.Initialize(origin);
	//	movable = origin.GetComponent<Movable>();
	//	crouchable = origin.GetComponent<Crouchable>();
	//}
}
