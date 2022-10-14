using UnityEngine;

public class PlayerStateSO : StateSO
{
	[SerializeField] protected PlayerStatesManagerSO states;
	[SerializeField] protected PlayerAbilitiesManagerSO abilities;
	protected PlayerInputReaderSO inputReader;

	protected Player player;

	public void Initialize(Player player)
	{
		InitializeMachine(player.sm);
		InitializeAnimator(player.anim);
		this.player = player;
		inputReader = player.inputReader;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		checks.Add(() =>
		{
			player.DoChecks();
		});
	}
}
