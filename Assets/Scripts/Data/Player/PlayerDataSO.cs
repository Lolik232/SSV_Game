using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]

public class PlayerDataSO : DataSO
{
	[HideInInspector] public new PlayerStatesManagerSO states;
	[HideInInspector] public new PlayerCheckersManagerSO checkers;
	[HideInInspector] public new PlayerAbilitiesManagerSO abilities;
	[HideInInspector] public new PlayerParametersManagerSO parameters;
	[HideInInspector] public new WeaponsManagerSO weapons;
	[HideInInspector] public new PlayerInputReaderSO controller;

	protected override void OnEnable()
	{
		states = (PlayerStatesManagerSO)base.states;
		checkers = (PlayerCheckersManagerSO)base.checkers;
		abilities = (PlayerAbilitiesManagerSO)base.abilities;
		parameters = (PlayerParametersManagerSO)base.parameters;
		weapons = (WeaponsManagerSO)base.weapons;
		controller = (PlayerInputReaderSO)base.controller;

		base.OnEnable();
	}
}
