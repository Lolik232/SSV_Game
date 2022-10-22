using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InventoryBase : BaseMonoBehaviour
{
	[SerializeField] private InventorySO _inventory;

	protected override void Awake()
	{
		startActions.Add(() =>
		{
			_inventory.Initialize();
		});

		updateActions.Add(() =>
		{
			_inventory.CurrentWeapon.OnUpdate();
		});
	}
}
