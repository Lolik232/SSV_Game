using System;

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

[Serializable]
public struct PickableColor
{
	[SerializeField] private float _r;
	[SerializeField] private float _g;
	[SerializeField] private float _b;
	[SerializeField] private float _a;

	public Color Color
	{
		get => new Color(_r, _g, _b, _a);
		set
		{
			_r = value.r;
			_g = value.g;
			_b = value.b;
			_a = value.a;
		}
	}
}