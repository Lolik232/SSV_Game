using UnityEngine;

public abstract class PlayerAbilitySO : AbilitySO
{
	[SerializeField] protected PlayerDataSO data;

	protected Player player;

	public void Initialize(Player player, Animator anim)
	{
		InitializeAnimator(anim);
		this.player = player;
	}
}
