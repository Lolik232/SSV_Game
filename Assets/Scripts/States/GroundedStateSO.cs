using System;

public abstract class GroundedStateSO : StateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		transitions.Add(new TransitionItem(entity.states.inAir, InAirCondition, InAirAction));
	}

	protected virtual bool InAirCondition()
	{
		return !entity.checkers.grounded;
	}

	protected virtual void InAirAction()
	{
		if (entity.checkers.touchingCeiling && !entity.isStanding)
		{
			entity.MoveToY(entity.Position.y - (entity.StandSize.y - entity.CrouchSize.y));
		}
	}
}
