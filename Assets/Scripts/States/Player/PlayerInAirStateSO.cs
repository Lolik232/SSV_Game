using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "States/In Air/Player")]

public class PlayerInAirStateSO : InAirStateSO
{
	[HideInInspector] protected new PlayerSO entity;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();
	}
}
