using System.Collections;

using UnityEngine;

public abstract class MoveAS<AbilityT> : AbilityState<AbilityT> where AbilityT : Ability
{
	[SerializeField] private float _acceleration;
	[SerializeField] private float _maxSpeed;

	private float _delta;

	protected float MoveSpeed
	{
		get;
		private set;
	}
	protected float StartSpeed
	{
		private get;
		set;
	}
	protected float EndSpeed
	{
		private get;
		set;
	}
	protected int MoveDirection
	{
		private get;
		set;
	}

	private float Acceleration
	{
		get;
		set;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		EndSpeed = MoveDirection * _maxSpeed;
		MoveSpeed = StartSpeed;
		_delta = EndSpeed - StartSpeed;
		Acceleration = Mathf.Sign(_delta) * Mathf.Abs(_acceleration);


		StartCoroutine(Accelerate());
	}

	private IEnumerator Accelerate()
	{
		if (Acceleration == 0f)
		{
			MoveSpeed = EndSpeed;
			yield break;
		}

		while (IsActive && MoveSpeed != EndSpeed)
		{
			yield return null;
			MoveSpeed = Mathf.Lerp(StartSpeed, EndSpeed, Mathf.Clamp((MoveSpeed + Acceleration * Time.deltaTime - StartSpeed) / _delta, 0, 1));
		}
	}
}
