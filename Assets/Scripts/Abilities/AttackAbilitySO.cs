using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility", menuName = "Abilities/Attack")]

public class AttackAbilitySO : AbilitySO
{
	[SerializeField] private InventorySO _inventory;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterConditions.Add(() => entity.controller.attack);

		enterActions.Add(() =>
		{
			entity.controller.attack = false;
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
