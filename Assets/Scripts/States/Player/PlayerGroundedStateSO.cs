using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGroundedState", menuName = "States/Grounded/Player")]

public class PlayerGroundedStateSO : GroundedStateSO
{
	[HideInInspector] protected new PlayerSO entity;

	[NonSerialized] public bool isClimbFinish;

	protected override void OnEnable()
	{
		entity = (PlayerSO)base.entity;

		base.OnEnable();

		updateActions.Add(() =>
		{

		});
	}
}
