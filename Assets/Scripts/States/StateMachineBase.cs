using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : BaseMonoBehaviour
{
	[SerializeField] private StateMachineSO _machine;

	protected Animator anim;

	protected override void Awake()
	{
		anim = GetComponent<Animator>();

		base.Awake();

		startActions.Add(() =>
		{
			_machine.Initialize();
		});

		updateActions.Add(() =>
		{
			_machine.CurrentState.OnStateUpdate();
		});

		fixedUpdateActions.Add(() =>
		{
			_machine.CurrentState.OnStateFixedUpdate();
		});
	}

	private void OnStateAnimationFinishTrigger() => _machine.CurrentState.OnStateAnimationFinishTrigger();

	private void OnStateAnimationTrigger() => _machine.CurrentState.OnStateAnimationTrigger();
}
