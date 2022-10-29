using System.Collections;
using System.Collections.Generic;
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
}
