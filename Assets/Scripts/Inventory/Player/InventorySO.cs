using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Player/Inventory")]

public class InventorySO : ScriptableObject
{
	[SerializeField] private WeaponSO _defaultWeapon;

	public WeaponSO CurrentWeapon { get; private set; }

	public void Initialize() => ChangeWeapon(_defaultWeapon);

	public void ChangeWeapon(WeaponSO newWeapon)
	{
		CurrentWeapon = newWeapon;
	}
}
