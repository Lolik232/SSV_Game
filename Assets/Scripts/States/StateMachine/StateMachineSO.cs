using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMachine", menuName = "Player/States/State Machine")]

public class StateMachineSO : ScriptableObject
{
	[SerializeField] private StateSO _defaultState;

	public StateSO CurrentState { get; private set; }

	public void Initialize() => GetTransitionState(_defaultState);

	public void GetTransitionState(StateSO transitionState)
	{
		if (CurrentState != null)
		{
			CurrentState.OnStateExit();
		}

		CurrentState = transitionState;
		CurrentState.OnStateEnter();
	}
}
