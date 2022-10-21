using UnityEngine;

[CreateAssetMenu(fileName = "PayerAttackAbility", menuName = "Player/Abilities/Attack")]

public class PlayerAttackAbilitySO : PlayerAbilitySO
{
	[SerializeField] private InventorySO _inventory;

	protected override void OnEnable()
	{
		base.OnEnable();

		useConditions.Add(() => data.input.attackInput);

		useActions.Add(() =>
		{
			data.input.attackInput = false;
			_inventory.CurrentWeapon.OnWeaponEnter();
		});

		terminateActions.Add(() =>
		{
			_inventory.CurrentWeapon.OnWeaponExit();
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
