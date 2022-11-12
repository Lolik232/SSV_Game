using UnityEngine;

public class PlayerAttackAS : AbilityState<PlayerAttackAbility>
{
	private Inventory _inventory;

	protected override void Awake()
	{
		base.Awake();
		_inventory = GetComponentInChildren<Inventory>();
	}

	protected override void ApplyEnterActions()
	{
		base.ApplyEnterActions();
		_inventory.Current.OnEnter();
	}

	protected override void ApplyExitActions()
	{
		base.ApplyExitActions();
		_inventory.Current.OnExit();
	}
}
