using UnityEngine;

public abstract class InAirStateSO : StateSO
{
	protected override void OnEnable()
	{
		base.OnEnable();

		transitions.Add(new TransitionItem(entity.states.grounded, GroundedCondition, GroundedAction));
	}

	protected virtual bool GroundedCondition()
	{
		return entity.checkers.grounded &&
					 entity.Velocity.y < 0.01f;
	}

	protected virtual void GroundedAction()
	{

	}
}
