using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChecker
{
	public const float CHECK_OFFSET = 0.02f;
	public const float UNIT_SIZE = 1f;

	public void UpdateCheckersPosition();

	public void DoChecks();
}

public struct CheckArea
{
	public Vector2 a;
	public Vector2 b;

	public CheckArea(Vector2 a, Vector2 b)
	{
		this.a = a;
		this.b = b;
	}

	public CheckArea(float aX, float aY, float bX, float bY)
	{
		a = new Vector2(aX, aY);
		b = new Vector2(bX, bY);
	}
}