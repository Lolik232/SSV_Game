using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase : BaseMonoBehaviour
{
	[SerializeField] private StatesManagerSO _statesManager;
	[SerializeField] private StateMachineSO _machine;

	protected Animator anim;

	protected override void Awake()
	{
		anim = GetComponent<Animator>();

		base.Awake();

		startActions.Add(() =>
		{
			_statesManager.Initialize(anim);
			_machine.Initialize();
		});

		updateActions.Add(() =>
		{
			_machine.CurrentState.OnUpdate();
		});

		fixedUpdateActions.Add(() =>
		{
			_machine.CurrentState.OnFixedUpdate();
		});
	}

	private void OnStateAnimationFinishTrigger() => _machine.CurrentState.OnAnimationFinishTrigger();

	private void OnStateAnimationTrigger() => _machine.CurrentState.OnAnimationTrigger();
}
