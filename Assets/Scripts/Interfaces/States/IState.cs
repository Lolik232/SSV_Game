using System.Collections.Generic;

public interface IState
{
	public State Default
	{
		get;
	}
	public List<TransitionItem> Transitions
	{
		get;
	}

	public List<BlockedAbility> PermitedAbilities
	{
		get;
	}
}
