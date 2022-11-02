using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private List<State> _states = new();

	private State _current;

	public State Current
	{
		get => _current;
		private set => _current = value;
	}

	private void Awake()
	{
		GetComponents(_states);
	}

	private void Start()
	{
		GetTransition(_states.First());
	}

	private void Update()
	{
		StartCoroutine(WaitForChecks());
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

	private IEnumerator WaitForChecks()
	{
		yield return new WaitForFixedUpdate();
		TryGetTransition();
	}
}
