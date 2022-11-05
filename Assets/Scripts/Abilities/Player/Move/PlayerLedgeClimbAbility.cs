using System.Collections;

using UnityEngine;

public class PlayerLedgeClimbAbility : Ability
{
	[SerializeField] private float _duration;

	protected override void Awake()
	{
		base.Awake();
		exitConditions.Add(() => ActiveTime > _duration);
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		StartCoroutine(SetAnimationSpeed());
	}

	private IEnumerator SetAnimationSpeed()
	{
		anim.SetFloat("ledgeClimbSpeed", 1f);

		yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("LedgeClimb"));

		float animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
		anim.SetFloat("ledgeClimbSpeed", animDuration / _duration);
	}
}
