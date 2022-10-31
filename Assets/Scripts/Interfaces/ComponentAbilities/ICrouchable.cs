using UnityEngine;

public interface ICrouchable
{
	public Vector2 StandSize
	{
		get;
	}

	public Vector2 StandOffset
	{
		get;
	}

	public Vector2 StandCenter
	{
		get;
	}

	public Vector2 CrouchSize
	{
		get;
	}

	public Vector2 CrouchOffset
	{
		get;
	}

	public Vector2 CrouchCenter
	{
		get;
	}

	public bool IsStanding
	{
		get;
	}

	public void Stand();

	public void Crouch();
}
