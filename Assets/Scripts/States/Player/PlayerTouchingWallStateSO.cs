using System;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTouchingWallState", menuName = "States/Touching Wall/Player")]

public class PlayerTouchingWallStateSO : StateSO
{
	[SerializeField] protected InAirStateSO inAir;
	[SerializeField] protected GroundedStateSO grounded;
	[Space]

	protected Movable movable;

	protected override void OnEnable()
	{
		base.OnEnable();

		transitions.Add(new TransitionItem(inAir, InAirCondition, InAirAction));
		transitions.Add(new TransitionItem(grounded, GroundedCondition, GroundedAction));
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		movable = origin.GetComponent<Movable>();
	}

	protected virtual bool InAirCondition()
	{
		return !entity.checkers.touchingWall ||
					 (entity.controller.move.x != movable.FacingDirection && !entity.controller.grab);
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
