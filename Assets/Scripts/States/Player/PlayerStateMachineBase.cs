using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerStateMachineBase : StateMachineBase
{
	[SerializeField] private PlayerStatesManagerSO _statesManager;

	private Player _player;

	protected override void Awake()
	{
		base.Awake();
		_player = GetComponent<Player>();
	}

	protected override void Start()
	{
		_statesManager.Initialize(_player, anim);
		base.Start();
	}
}
