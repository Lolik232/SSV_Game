using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface IRotateable
{
	public int FacingDirection
	{
		get;
	}
	public int BodyDirection
	{
		get;
	}

	public void TryRotateIntoDirection(int direction);

	public void RotateBodyIntoDirection(int direction);

	public void ReleaseDirection(int id);

	public int HoldDirection(int direction);
}
