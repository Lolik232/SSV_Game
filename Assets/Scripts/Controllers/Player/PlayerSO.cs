using System;

using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Controllers/Entity/Player")]

public class PlayerSO : EntitySO
{
	[HideInInspector] [NonSerialized] public new PlayerAbilitiesManagerSO abilities;
	[HideInInspector] [NonSerialized] public new PlayerStateMachineSO states;
	[HideInInspector] [NonSerialized] public new PlayerCheckersManagerSO checkers;
	[HideInInspector] [NonSerialized] public new PlayerParametersManagerSO parameters;
	[HideInInspector] [NonSerialized] public new PlayerInputReaderSO controller;

	public WeaponsManagerSO weapons;

	protected override void OnEnable()
	{
		checkers = (PlayerCheckersManagerSO)base.checkers;
		abilities = (PlayerAbilitiesManagerSO)base.abilities;
		states = (PlayerStateMachineSO)base.states;
		parameters = (PlayerParametersManagerSO)base.parameters;
		controller = (PlayerInputReaderSO)base.controller;

		base.OnEnable();
	}
}
