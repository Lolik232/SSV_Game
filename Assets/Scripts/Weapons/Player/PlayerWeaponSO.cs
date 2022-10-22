using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSO : WeaponSO
{
	protected new PlayerDataSO data => (PlayerDataSO)base.data;

	protected override void OnEnable()
	{
		base.OnEnable();
	}
}
