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
	public int FacingDirection
	{
		get;
	}
	public int BodyDirection
	{
		get;
	}

	public float MoveUpSpeed
	{
		get; set;
	}

	public float MoveDownSpeed
	{
		get; set;
	}

	public float MoveForwardSpeed
	{
		get; set;
	}

	public float MoveBackwardSpeed
	{
		get; set;
	}

	public void TrySetPosition(Vector2 position);

	public void TrySetPositionX(float x);

	public void TrySetPositionY(float y);

	public void TrySetVelocity(Vector2 velocity);

	public void TrySetVelocity(float speed, Vector2 angle, int direction);

	public void TrySetVelocityX(float x);

	public void TrySetVelocityY(float y);

	public void TrySetGravity(float gravity);

	public void TryRotateIntoDirection(int direction);

	public void RotateBodyIntoDirection(int direction);

	public int HoldPosition(Vector2 position);

	public int HoldVelocity(Vector2 velocity);

	public int HoldVelocity(float speed, Vector2 angle, int direction);

	public int HoldGravity(float gravity);

	public int HoldDirection(int direction);

	public void ReleasePosition(int id);

	public void ReleaseVelocity(int id);

	public void ReleaseGravity(int id);

	public void ReleaseDirection(int id);
}
