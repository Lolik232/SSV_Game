using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotateable : MonoBehaviour, IRotateable
{
	private readonly Cacheable<int> _facingDirection = new();
	private int _bodyDirection;

	public int FacingDirection
	{
		get => _facingDirection.Value;
	}
	public int BodyDirection
	{
		get => _bodyDirection;
	}

	private void Start()
	{
		TryRotateIntoDirection(-1);
	}

	public void ReleaseDirection(int id)
	{
		_facingDirection.Release(id);
		RotateBodyIntoDirection(FacingDirection);
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

	public void TryRotateIntoDirection(int direction)
	{
		if (direction != 0)
		{
			if (_facingDirection.TrySet(direction))
			{
				RotateBodyIntoDirection(direction);
			}
		}
	}

	public int HoldDirection(int direction)
	{
		int id = _facingDirection.Hold(direction);
		RotateBodyIntoDirection(direction);
		return id;
	}
}
