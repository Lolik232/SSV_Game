using UnityEngine;

[RequireComponent(typeof(WallChecker), typeof(Physical))]

public class WallAbility : Ability
{
	protected Vector2 _holdPosition;

	private WallChecker _wallChecker;
	private Physical _physical;
	protected override void Awake()
	{
		base.Awake();
		_wallChecker = GetComponent<WallChecker>();
		_physical = GetComponent<Physical>();
	}
}
