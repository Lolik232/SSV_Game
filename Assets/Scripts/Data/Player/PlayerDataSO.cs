using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]

public class PlayerDataSO : ScriptableObject
{
	public PlayerStatesManagerSO states;
	public PlayerCheckersManagerSO checkers;
	public PlayerAbilitiesManagerSO abilities;
	public PlayerParametersManagerSO parameters;
	public PlayerWeaponsManagerSO weapons;
	public PlayerInputReaderSO input;
}
