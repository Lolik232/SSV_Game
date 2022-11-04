using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private bool _transitionChecked;

	private List<State> _states = new();

	private State _current;

	public State Current
	{
		get => _current;
		private set => _current = value;
	}

	public bool TransitionsChecked
	{
		get => _transitionChecked;
		set => _transitionChecked = value;
	}

	private void Awake()
	{
		GetComponents(_states);
	}

	private void Start()
	{
		Initialize(_states.First());
	}

	private void Update()
	{
		StartCoroutine(TryGetTransitions());
	}

	private void TryGetTransition()
	{
		if (!Current.IsActive)
		{
			return;
		}

		if (Current.IsLocked)
		{
			GetTransition(Current, Current.Default);
			return;
		}

		foreach (var transition in Current.Transitions)
		{
			if (transition.DoChecks())
			{
				GetTransition(Current, transition.target);
				return;
			}
		}
	}

	private void Initialize(State target)
	{
		foreach (var ability in target.PermitedAbilities)
		{
			ability.component.Unlock();
		}

		Current = target;
		Current.OnEnter();
	}

	private void GetTransition(State origin, State target)
	{
		Current.OnExit();
		foreach (var ability in origin.PermitedAbilities)
		{
			if (!target.PermitedAbilities.Contains(ability))
			{
				ability.component.Block();
			}
		}

		Current = target;
		Current.OnEnter();
		foreach (var ability in target.PermitedAbilities)
		{
			if (!origin.PermitedAbilities.Contains(ability))
			{
				ability.component.Unlock();
			}
		}
	}

	private IEnumerator TryGetTransitions()
	{
		yield return new WaitForFixedUpdate();
		TryGetTransition();
		TransitionsChecked = true;
	}
}
