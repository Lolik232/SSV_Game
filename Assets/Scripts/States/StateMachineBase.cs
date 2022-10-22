using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
	[SerializeField] private StateMachineSO _machine;

	protected Animator anim;

	protected virtual void Awake()
	{
		anim = GetComponent<Animator>();

	}

	protected virtual void Start()
	{
		_machine.Initialize();
	}

	private void Update()
	{
		_machine.CurrentState.OnStateUpdate();
	}

	private void FixedUpdate()
	{
		_machine.CurrentState.OnStateFixedUpdate();
	}

	private void OnStateAnimationFinishTrigger() => _machine.CurrentState.OnStateAnimationFinishTrigger();

	private void OnStateAnimationTrigger() => _machine.CurrentState.OnStateAnimationTrigger();
}
