using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGroundedState", menuName = "States/Grounded/Player")]

public class PlayerGroundedStateSO : GroundedStateSO
{
	[HideInInspector] [NonSerialized] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		transitions.Add(new TransitionItem(entity.states.touchingWall, TouchingWallCondition, TouchingWallAction));
	}

	protected virtual bool TouchingWallCondition()
	{
		return !entity.checkers.touchingCeiling && entity.controller.grab;
	}

	protected virtual void TouchingWallAction()
	{

	}
}
