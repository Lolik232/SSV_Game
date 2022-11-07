using System.Collections;

using UnityEngine;

public class StayAS<AbilityT> : AbilityState<AbilityT> where AbilityT : Ability
{
	[SerializeField] private float _deceleration;

	private float _decelerationTime;
	private float _decelerationStartTime;

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
	private float DecelerationTime
	{
		get => Time.time - _decelerationStartTime;
		set => _decelerationStartTime = value;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		EndSpeed = 0f;
		MoveSpeed = StartSpeed;
		DecelerationTime = Time.time;
		if (_deceleration != 0f)
		{
			_decelerationTime = Mathf.Abs((EndSpeed - StartSpeed) / _deceleration);
			StartCoroutine(Decelerate());
		}
		else
		{
			MoveSpeed = EndSpeed;
		}
	}

	private IEnumerator Decelerate()
	{
		while (IsActive && MoveSpeed != EndSpeed)
		{
			yield return null;
			MoveSpeed = Mathf.Lerp(StartSpeed, EndSpeed, DecelerationTime / _decelerationTime);
		}
	}
}
