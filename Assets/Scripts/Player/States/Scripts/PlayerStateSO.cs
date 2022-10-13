using UnityEngine;

public class PlayerStateSO : StateSO
{
	[SerializeField] protected PlayerStatesManagerSO states;
	[SerializeField] protected PlayerAbilitiesManagerSO abilities;
	protected Player Player
	{
		get; private set;
	}

	public void Initialize(Player player)
	{
		InitializeMachine(player.Machine);
		InitializeAnimator(player.Anim);
		Player = player;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		checks.Add(() =>
		{
			Player.DoChecks();
		});
	}
}
