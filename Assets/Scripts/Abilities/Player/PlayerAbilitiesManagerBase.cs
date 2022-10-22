using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManagerBase : BaseMonoBehaviour
{
	[SerializeField] private PlayerAbilitiesManagerSO _abilitiesManager;

	private Player _player;
	private Animator _anim;

	protected override void Awake()
	{
		_player = GetComponent<Player>();
		_anim = GetComponent<Animator>();

		base.Awake();

		startActions.Add(()=>
		{
			_abilitiesManager.Initialize(_player, _anim);
		});


		updateActions.Add(() =>
		{
			bool abilityUsed = false;
			foreach (var ability in _abilitiesManager.abilities)
			{
				if (!abilityUsed)
				{
					abilityUsed |= ability.TryUseAbility();
				}

				ability.OnUpdate();
			}
		});
	}
	private void OnAttackAnimationFinishTrigger() => _abilitiesManager.attack.Terminate();
}
