using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManagerBase : AbilitiesManagerBase
{
	protected new PlayerAbilitiesManagerSO abilitiesManager => (PlayerAbilitiesManagerSO)base.abilitiesManager;

	protected override void Awake()
	{
		base.Awake();
	}

	private void OnAttackAnimationFinishTrigger() => abilitiesManager.attack.Terminate();
}
