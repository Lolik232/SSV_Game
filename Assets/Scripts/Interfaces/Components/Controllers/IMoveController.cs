using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveController
{
	public Vector2Int Move
	{
		get;
	}

	public Vector2 LookAt
	{
		get;
	} 
}
