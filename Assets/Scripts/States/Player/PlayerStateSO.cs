using UnityEngine;

public abstract class PlayerStateSO : StateSO
{
	[SerializeField] protected PlayerDataSO data;

	protected Player player;

	public void Initialize(Player player, Animator anim)
	{
		InitializeAnimator(anim);
		this.player = player;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		checks.Add(() =>
		{
			data.checkers.DoChecks();
		});
	}
}
