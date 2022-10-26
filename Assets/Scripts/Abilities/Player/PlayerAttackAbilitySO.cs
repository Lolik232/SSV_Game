using UnityEngine;

[CreateAssetMenu(fileName = "PayerAttackAbility", menuName = "Player/Abilities/Attack")]

public class PlayerAttackAbilitySO : AbilitySO
{
	[SerializeField] private InventorySO _inventory;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() => data.controller.attack);

		enterActions.Add(() =>
		{
			data.controller.attack = false;
			_inventory.CurrentWeapon.OnEnter();
		});

		exitActions.Add(() =>
		{
			_inventory.CurrentWeapon.OnExit();
		});
	}

	public void HoldDirection(int direction)
	{
		_inventory.CurrentWeapon.HoldDirection(direction);
	}

	public void ReleaseDirection()
	{
		_inventory.CurrentWeapon.ReleaseDirection();
	}
}
