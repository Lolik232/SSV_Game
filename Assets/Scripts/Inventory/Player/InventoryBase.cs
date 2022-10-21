using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour
{
	[SerializeField] private InventorySO _inventory;

	protected virtual void Awake()
	{

	}

	protected virtual void Start()
	{
		_inventory.Initialize();
	}

	private void Update()
	{
		_inventory.CurrentWeapon.OnUpdate();
	}
}
