using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManagerBase : BaseMonoBehaviour
{
	[SerializeField] private AbilitiesManagerSO _abilitiesManager;

	private Animator _anim;
	protected override void Awake()
	{
		_anim = GetComponent<Animator>();

		base.Awake();

		startActions.Add(()=>
		{
			_abilitiesManager.Initialize(_anim);
		});

		updateActions.Add(() =>
		{
			bool abilityUsed = false;
			foreach (var ability in _abilitiesManager.abilities)
			{
				if (!abilityUsed)
				{
					ability.OnEnter();
				}

				ability.OnUpdate();
			}
		});
	}

	private void OnAttackAnimationFinishTrigger() => _abilitiesManager.OnAttackAnimationFinishTrigger();
}
