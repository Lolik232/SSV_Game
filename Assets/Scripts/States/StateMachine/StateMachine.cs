using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private bool _transitionChecked;

	private readonly List<State> _states = new();
	private readonly List<IChecker> _checkers = new();

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
		GetComponents(_checkers);
	}

	private void Start()
	{
		Initialize(_states.First());
	}

	private void Update()
	{
		TryGetTransition();
		TransitionsChecked = true;
	}

	private void TryGetTransition()
	{
		if (!Current.IsActive)
		{
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

	private void Initialize(State target)
	{
		foreach (var ability in target.PermitedAbilities)
		{
			ability.component.Unlock();
		}

		Current = target;
		Current.OnEnter();
	}

	public void GetTransition(State target)
	{
		GetTransition(Current, target);
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

		foreach (var checker in _checkers)
		{
			checker.UpdateCheckersPosition();
			checker.DoChecks();
		}

		foreach (var ability in target.PermitedAbilities)
		{
			if (!origin.PermitedAbilities.Contains(ability))
			{
				ability.component.Unlock();
			}
		}
	}
}
