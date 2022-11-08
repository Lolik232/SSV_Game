using UnityEngine;

public class Rotateable : MonoBehaviour, IRotateable
{
	public int FacingDirection
	{
		get;
		private set;
	}
	public int BodyDirection
	{
		get;
		private set;
	}

	private void Start()
	{
		RotateIntoDirection(-1);
	}

	public void RotateBodyIntoDirection(int direction)
	{
		if (direction != 0)
		{
			BodyDirection = direction;
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
			FacingDirection = direction;
			RotateBodyIntoDirection(direction);
		}
	}
}
