using System;

using UnityEngine;

public class PlayerAbilitySO : AbilitySO
{
	[SerializeField] protected PlayerInputReaderSO inputReader;
	[SerializeField] protected PlayerParametersManagerSO parameters;

	protected Player player;

	public void Initialize(Player player)
	{
		InitializeAnimator(player.anim);
		this.player = player;
	}
}
