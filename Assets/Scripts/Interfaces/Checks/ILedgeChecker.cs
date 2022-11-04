using UnityEngine;

public interface ILedgeChecker : IChecker
{
	public bool TouchingLegde
	{
		get;
	}
	public bool TouchingGround
	{
		get;
	}
	public Vector2 GroundPosition
	{
		get;
	}
	public float YOffset
	{
		get;
	}
}
