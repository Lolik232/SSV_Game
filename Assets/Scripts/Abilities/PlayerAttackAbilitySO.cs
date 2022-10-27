using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackAbility", menuName = "Abilities/Attack/Player")]

public class PlayerAttackAbilitySO : AbilitySO
{
	[SerializeField] private InventorySO _inventory;

	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

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
