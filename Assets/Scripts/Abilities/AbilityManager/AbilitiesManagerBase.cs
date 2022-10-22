using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManagerBase : BaseMonoBehaviour
{
	[SerializeField] protected AbilitiesManagerSO abilitiesManager;

	private Animator _anim;
	protected override void Awake()
	{
		_anim = GetComponent<Animator>();

		base.Awake();

		startActions.Add(()=>
		{
			abilitiesManager.Initialize(_anim);
		});


		updateActions.Add(() =>
		{
			bool abilityUsed = false;
			foreach (var ability in abilitiesManager.abilities)
			{
				if (!abilityUsed)
				{
					abilityUsed |= ability.TryUseAbility();
				}

				ability.OnUpdate();
			}
		});
	}
}
