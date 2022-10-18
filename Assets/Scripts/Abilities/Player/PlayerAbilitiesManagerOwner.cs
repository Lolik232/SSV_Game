using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManagerOwner : MonoBehaviour
{
	[SerializeField] private PlayerAbilitiesManagerSO _abilitiesManager;

	private Player _player;

	private void Awake()
	{
		_player = GetComponent<Player>();
	}

	private void Start()
	{
		_abilitiesManager.Initialize(_player);
	}

	private void Update()
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
	}

	private void OnAttackAnimationFinishTrigger() => _abilitiesManager.attack.Terminate();
}
