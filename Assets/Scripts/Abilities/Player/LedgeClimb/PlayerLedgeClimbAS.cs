using System.Collections;

using UnityEngine;

public class PlayerLedgeClimbAS : AbilityState<PlayerLedgeClimbAbility>
{
	[SerializeField] private float _duration;

	private PlayerLedgeClimbAbility _ability;

	protected override void Awake()
	{
		base.Awake();

		_ability = GetComponent<PlayerLedgeClimbAbility>();

		SetAnimationSpeed(_duration);
	}

	protected override void ApplyUpdateActions()
	{
		base.ApplyUpdateActions();

		if (ActiveTime > _duration)
		{
			_ability.OnExit();
		}
	}
}
