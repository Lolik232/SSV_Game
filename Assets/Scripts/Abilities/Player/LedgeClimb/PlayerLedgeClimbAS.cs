using UnityEngine;

public class PlayerLedgeClimbAS : AbilityState<PlayerLedgeClimbAbility>
{
	[SerializeField] private float _duration;

	private void Start()
	{
		SetAnimationSpeed(_duration);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();

		if (ActiveTime > _duration)
		{
			Ability.OnExit();
		}
	}
}
