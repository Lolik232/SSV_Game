using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public class AbilitiesManager : MonoBehaviour
{
	private readonly List<Ability> _abilities = new();

	private StateMachine _stateMachine;

	private void Awake()
	{
		GetComponents(_abilities);
		_stateMachine = GetComponent<StateMachine>();
	}

	private void Update()
	{
		StartCoroutine(ApplyAbilities());
	}

	private IEnumerator ApplyAbilities()
	{
		yield return new WaitUntil(() => _stateMachine.TransitionsChecked);

		_stateMachine.TransitionsChecked = false;
		foreach (var ability in _abilities)
		{
			ability.TryEnter();
			ability.OnUpdate();
		}
	}
}