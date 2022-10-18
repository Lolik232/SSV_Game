using UnityEngine;

public class PlayerStateSO : StateSO
{
	[SerializeField] protected PlayerStatesManagerSO states;
	[SerializeField] protected PlayerAbilitiesManagerSO abilities;
	[SerializeField] protected PlayerParametersManagerSO parameters;
	[SerializeField] protected PlayerInputReaderSO inputReader;

	protected Player player;

	public void Initialize(Player player)
	{
		InitializeMachine(player.machine);
		InitializeAnimator(player.anim);
		this.player = player;
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
