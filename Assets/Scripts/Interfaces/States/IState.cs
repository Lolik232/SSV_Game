using System.Collections.Generic;

public interface IState
{
	public List<StateTransitionItem> Transitions
	{
		get;
	}

	public List<BlockedAbility> PermitedAbilities
	{
		get;
	}
}
