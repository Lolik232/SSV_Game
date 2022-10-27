using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Controllers/Entity/Player")]

public class PlayerSO : EntitySO
{
	[HideInInspector] public new PlayerAbilitiesManagerSO abilities;
	[HideInInspector] public new PlayerStateMachineSO states;
	[HideInInspector] public new PlayerCheckersManagerSO checkers;
	[HideInInspector] public new PlayerParametersManagerSO parameters;
	[HideInInspector] public new PlayerInputReaderSO controller;

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
