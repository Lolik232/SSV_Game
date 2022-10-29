using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTouchingWallState", menuName = "States/Touching Wall/Player")]

public class PlayerTouchingWallStateSO : StateSO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		requiredCondition = () => entity.checkers.touchingWall && entity.checkers.touchingLedge;

		transitions.Add(new TransitionItem(entity.states.inAir, InAirCondition, InAirAction));
		transitions.Add(new TransitionItem(entity.states.grounded, GroundedCondition, GroundedAction));
	}

	protected virtual bool InAirCondition()
	{
		return !entity.checkers.touchingWall ||
					 (entity.controller.move.x != entity.direction.facing && !entity.controller.grab);
	}

	protected virtual bool GroundedCondition()
	{
		return !entity.controller.grab;
	}

	protected virtual void InAirAction()
	{

	}

	protected virtual void GroundedAction()
	{

	}
}
