using UnityEngine;

public class Rotateable : MonoBehaviour, IRotateable
{
	private int _facingDirection;
	private int _bodyDirection;

	public int FacingDirection
	{
		get => _facingDirection;
	}
	public int BodyDirection
	{
		get => _bodyDirection;
	}

	private void Start()
	{
		RotateIntoDirection(-1);
	}

	public void RotateBodyIntoDirection(int direction)
	{
		if (direction != 0)
		{
			_bodyDirection = direction;
			switch (direction)
			{
				case 1:
					transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					break;
				case -1:
					transform.rotation = Quaternion.Euler(0f, 180f, 0f);
					break;
				default:
					break;
			}
		}
	}

	public void RotateIntoDirection(int direction)
	{
		if (direction != 0)
		{
			_facingDirection = direction;
			RotateBodyIntoDirection(direction);
		}
	}
}
