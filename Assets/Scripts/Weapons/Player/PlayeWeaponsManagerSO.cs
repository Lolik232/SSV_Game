using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponsManager", menuName = "Weapons/Manager/Player")]

public class PlayeWeaponsManagerSO : WeaponsManagerSO
{
	public MeleeWeaponSO sword;

	private void OnEnable()
	{
		weapons = new List<WeaponSO>()
		{
			sword
		};
	}
}
