using UnityEngine;

public interface IMovable
{
	public Vector2 Position
	{
		get;
	}
	public Vector2 Velocity
	{
		get;
	}
	public float Gravity
	{
		get;
	}
	public bool IsPositionLocked
	{
		get;
	}
	public bool IsVelocityLocked
	{
		get;
	}

	public void SetPosition(Vector2 position);

	public void SetPosition(float x, float y);

	public void SetpositionX(float x);

	public void SetpositionY(float y);

	public void SetVelocity(Vector2 velocity);

	public void SetVelocity(float x, float y);

	public void SetVelocity(float spped, Vector2 angle, int direction);

	public void SetVelocityX(float x);

	public void SetVelocityY(float Y);

	public void SetGravity(float gravity);

	public void ResetGravity();

	public void BlockPosition();

	public void BlockVelocity();

	public void UnlockPosition();

	public void UnlockVelocity();
}
