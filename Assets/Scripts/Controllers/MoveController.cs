using UnityEngine;

public class MoveController : BaseMonoBehaviour, IMoveController
{
	public Vector2Int Move
	{
		get;
		set;
	}
	public Vector2 LookAt
	{
		get;
		set;
	}
}
