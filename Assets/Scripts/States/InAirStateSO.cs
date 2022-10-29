using UnityEngine;

public abstract class InAirStateSO : StateSO
{
	protected Movable movable;

	protected override void OnEnable()
	{
		base.OnEnable();

		requiredCondition = () => !entity.checkers.grounded;

		transitions.Add(new TransitionItem(entity.states.grounded, GroundedCondition, GroundedAction));
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		movable = origin.GetComponent<Movable>();
	}

	protected virtual bool GroundedCondition()
	{
		return movable.Velocity.y < 0.01f;
	}

	protected virtual void GroundedAction()
	{

	}
}
