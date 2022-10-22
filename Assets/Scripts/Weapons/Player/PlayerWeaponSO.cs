using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeaponSO : WeaponSO
{
	protected Player player;

	public void Initialize(Player player, Animator baseAnim, Animator anim, Hit hit)
	{
		InitializeBaseAnimator(baseAnim);
		InitializeAnimator(anim);
		InitializeHit(hit);
		this.player = player;
	}
}
