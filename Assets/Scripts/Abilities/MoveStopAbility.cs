using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Movable), typeof(Rotateable))]

public class MoveStopAbility : Ability
{
	[SerializeField] private float _deceleration;

	protected float realDeceleration;
	protected float moveSpeed;
	protected float startSpeed;
	protected float endSpeed;

	protected Movable movable;
	protected Rotateable rotateable;

	protected float Deceleration
	{
		get => _deceleration;
	}

	protected override void Awake()
	{
		base.Awake();
		movable = GetComponent<Movable>();
		rotateable = GetComponent<Rotateable>();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		endSpeed = 0f;
		moveSpeed = startSpeed;
		realDeceleration = Mathf.Sign(startSpeed) * Mathf.Abs(Deceleration);

		StartCoroutine(Decelerate());
	}

	private IEnumerator Decelerate()
	{
		if (Deceleration == 0f)
		{
			moveSpeed = endSpeed;
			yield break;
		}

		while (IsActive && moveSpeed != endSpeed)
		{
			yield return null;
			moveSpeed = Mathf.Lerp(startSpeed, endSpeed, (moveSpeed - realDeceleration - startSpeed) / (endSpeed - startSpeed));
		}
	}
}
