using UnityEngine;

public abstract class GroundedStateSO : StateSO
{
	protected Movable movable;
	protected Crouchable crouchable;

	protected override void OnEnable()
	{
		base.OnEnable();

		requiredCondition = () => entity.checkers.grounded;

		transitions.Add(new TransitionItem(entity.states.inAir, InAirCondition, InAirAction));
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		movable = origin.GetComponent<Movable>();
		crouchable = origin.GetComponent<Crouchable>();
	}

	protected virtual bool InAirCondition()
	{
		return true;
	}

	protected virtual void InAirAction()
	{
		if (entity.checkers.touchingCeiling && !crouchable.IsStanding)
		{
			movable.TrySetPositionY(movable.Position.y - (crouchable.StandSize.y - crouchable.CrouchSize.y));
		}
	}
}
