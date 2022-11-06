using System.Collections;

using UnityEngine;

public class StayAS<AbilityT> : AbilityState<AbilityT> where AbilityT : Ability
{
	[SerializeField] private float _deceleration;

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
	private float Deceleration
	{
		get;
		set;
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		EndSpeed = 0f;
		MoveSpeed = StartSpeed;
		Deceleration = Mathf.Sign(StartSpeed) * Mathf.Abs(_deceleration);

		StartCoroutine(Decelerate());
	}

	private IEnumerator Decelerate()
	{
		if (Deceleration == 0f)
		{
			MoveSpeed = EndSpeed;
			yield break;
		}

		while (IsActive && MoveSpeed != EndSpeed)
		{
			yield return null;
			MoveSpeed = Mathf.Lerp(StartSpeed, EndSpeed, (MoveSpeed - Deceleration - StartSpeed) / (EndSpeed - StartSpeed));
		}
	}
}
