using System.Collections;

using UnityEngine;

public abstract class MoveAS<AbilityT> : AbilityState<AbilityT> where AbilityT : Ability
{
	[SerializeField] private float _acceleration;
	[SerializeField] private float _maxSpeed;

	private float _accelerationTime;
	private float _accelerationStartTime;

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

	private float AccelerationTime
	{
		get => Time.time - _accelerationStartTime;
		set => _accelerationStartTime = value;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		EndSpeed = MoveDirection * _maxSpeed;
		MoveSpeed = StartSpeed;
		AccelerationTime = Time.time;
		if (_acceleration != 0f)
		{
			_accelerationTime = Mathf.Abs((EndSpeed - StartSpeed) / _acceleration);
			StartCoroutine(Accelerate());
		}
		else
		{
			MoveSpeed = EndSpeed;
		}
	}

	private IEnumerator Accelerate()
	{
		while (IsActive && MoveSpeed != EndSpeed)
		{
			yield return null;
			MoveSpeed = Mathf.Lerp(StartSpeed, EndSpeed, AccelerationTime / _accelerationTime);
		}
	}
}
