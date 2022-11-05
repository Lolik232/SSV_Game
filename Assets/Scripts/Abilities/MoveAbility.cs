using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Movable), typeof(Rotateable))]

public abstract class MoveAbility : Ability
{
	[SerializeField] private float _acceleration;
	[SerializeField] private float _maxSpeed;

	protected float realAcceleration;
	protected float moveSpeed;
	protected float startSpeed;
	protected float endSpeed;
	protected int moveDirection;

	protected Movable movable;
	protected Rotateable rotateable;

	private float Acceleration
	{
		get => _acceleration;
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
		endSpeed = moveDirection * _maxSpeed;
		moveSpeed = startSpeed;
		realAcceleration = Mathf.Sign(endSpeed - startSpeed) * Mathf.Abs(Acceleration);

		StartCoroutine(Accelerate());
	}

	private IEnumerator Accelerate()
	{
		if (Acceleration == 0f)
		{
			moveSpeed = endSpeed;
			yield break;
		}

		while (IsActive && moveSpeed != endSpeed)
		{
			yield return null;
			moveSpeed = Mathf.Lerp(startSpeed, endSpeed, (moveSpeed + realAcceleration - startSpeed) / (endSpeed - startSpeed));
		}
	}
}
