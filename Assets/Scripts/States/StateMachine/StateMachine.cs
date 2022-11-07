using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class StateMachine : MonoBehaviour
{
	private readonly List<State> _states = new();

	public State Current
	{
		get;
		private set;
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
		Current.OnUpdate();
	}

	private void Initialize(State target)
	{
		Current = target;
		Current.OnEnter();
	}

	public void GetTransition(State target)
	{
		Current.OnExit();
		Current = target;
		Current.OnEnter();
	}
}
