using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponsManagerSO : ScriptableObject
{
	private int _current;
	protected List<WeaponSO> weapons = new();

	public WeaponSO GetNextWeapon()
	{
		if (_current == weapons.Count - 1)
		{
			_current = 0;
		}
		else
		{
			_current++;
		}

		return weapons[_current];
	}

	public WeaponSO GetPrevWeapon()
	{
		if (_current == 0)
		{
			_current = weapons.Count - 1;
		}
		else
		{
			_current--;
		}

		return weapons[_current];
	}
}
