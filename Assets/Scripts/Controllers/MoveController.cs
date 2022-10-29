using UnityEngine;

public class MoveController : BaseMonoBehaviour, IMoveController
{
	protected Vector2Int move;
	protected Vector2 lookAt;

	public Vector2Int Move
	{
		get => move;
	}
	public Vector2 LookAt
	{
		get => lookAt;
	}
}
