using System;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "States/In Air/Player")]

public class PlayerInAirStateSO : InAirStateSO
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
		return entity.controller.grab || entity.controller.move.x == movable.FacingDirection;
	}

	protected virtual void TouchingWallAction()
	{

	}
}
