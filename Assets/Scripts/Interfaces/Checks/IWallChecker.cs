using UnityEngine;

public interface IWallChecker : IChecker
{
	public bool TouchingWall
	{
		get;
	}
	public bool TouchingWallBack
	{
		get;
	}
	public Vector2 WallPosition
	{
		get;
	}
	public int WallDirection
	{
		get;
	}
	public float YOffset
	{
		get;
	}
}
