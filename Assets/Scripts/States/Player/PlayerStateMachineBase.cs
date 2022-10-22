using All.Events;

using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerStateMachineBase : StateMachineBase
{
	[SerializeField] private PlayerStatesManagerSO _statesManager;

	private Player _player;

	protected override void Awake()
	{
		_player = GetComponent<Player>();

		base.Awake();

		startActions.Insert(0, () =>
		{
			_statesManager.Initialize(_player, anim);
		});
	}
}
