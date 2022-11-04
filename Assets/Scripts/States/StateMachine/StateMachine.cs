using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private readonly List<State> _states = new();
	private readonly List<IChecker> _checkers = new();

	private State _current;

	public State Current
	{
		get => _current;
		private set => _current = value;
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
		Current.OnUpdate();
	}

	private void Initialize(State target)
	{
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
		Current = target;
		Current.OnEnter();
	}
}
