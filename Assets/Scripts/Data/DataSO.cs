using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataSO : ScriptableObject
{
	public StatesManagerSO states;
	public CheckersManagerSO checkers;
	public AbilitiesManagerSO abilities;
	public ParametersManagerSO parameters;
	public WeaponsManagerSO weapons;
	public BehaviourControllerSO controller;

	protected virtual void OnEnable()
	{
	
	}
}
