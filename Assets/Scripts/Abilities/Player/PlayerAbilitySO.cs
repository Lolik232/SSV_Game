using System;

using UnityEngine;

public abstract class PlayerAbilitySO : AbilitySO
{
	protected new PlayerDataSO data => (PlayerDataSO)base.data;
}
