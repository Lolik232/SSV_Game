using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
	[SerializeField] private List<NamedState> _states;

	private State _current;

	public State Current
	{
		get => _current;
		private set => _current = value;
	}

	private void Start()
	{
		GetTransition(_states.First().state);
	}

	private void Update()
	{
		TryGetTransition();
	}

	private void TryGetTransition()
	{
		if (!Current.IsActive)
		{
			return;
		}

		if (Current.IsLocked)
		{
			GetTransition(Current.Default);
			return;
		}

		foreach (var transition in Current.Transitions)
		{
			if (transition.DoChecks())
			{
				GetTransition(transition.target);
				return;
			}
		}
	}

	private void GetTransition(State target)
	{
		if (Current is not null)
		{
			Current.OnExit();
		}

		Current = target;
		Current.OnEnter();
	}
}

[Serializable]
public struct NamedState
{
	[SerializeField] private string _description;
	public State state;
}
