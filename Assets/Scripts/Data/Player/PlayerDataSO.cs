using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]

public class PlayerDataSO : DataSO
{
	public new PlayerStatesManagerSO states => (PlayerStatesManagerSO)base.states;
	public new CheckersManagerSO checkers => (CheckersManagerSO)base.checkers;
	public new PlayerAbilitiesManagerSO abilities => (PlayerAbilitiesManagerSO)base.abilities;
	public new PlayerParametersManagerSO parameters => (PlayerParametersManagerSO)base.parameters;
	public new PlayerWeaponsManagerSO weapons => (PlayerWeaponsManagerSO)base.weapons;

	public PlayerInputReaderSO input;

	protected override void OnEnable()
	{
		base.OnEnable();
	}
}
