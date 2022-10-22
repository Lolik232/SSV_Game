using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataSO : ScriptableObject
{
	[SerializeField] protected StatesManagerSO states;
	[SerializeField] protected CheckersManagerSO checkers;
	[SerializeField] protected AbilitiesManagerSO abilities;
	[SerializeField] protected PlayerParametersManagerSO parameters;
	[SerializeField] protected PlayerWeaponsManagerSO weapons;

	protected virtual void OnEnable()
	{
	
	}
}
