using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMachine", menuName = "States/Machine")]

public class StateMachineSO : ScriptableObject
{
	[SerializeField] private StateSO _defaultState;

	public StateSO CurrentState { get; private set; }

	public void Initialize() => GetTransitionState(_defaultState);

	public void GetTransitionState(StateSO transitionState)
	{
		if (CurrentState != null)
		{
			CurrentState.OnExit();
		}

		CurrentState = transitionState;
		CurrentState.OnEnter();
		CurrentState.OnFixedUpdate();
	}
}
