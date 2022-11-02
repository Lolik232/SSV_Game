using UnityEngine;

public interface ILedgeChecker : IChecker
{
	public bool TouchingLegde
	{
		get;
	}
}
