using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponSO : AbilitySO
{
	[SerializeField] private string _weaponName;

	[SerializeField] protected Parameter attackDistance;
	[SerializeField] protected Parameter attackRadius;

	[SerializeField] protected HitSO hit;

	protected Vector2 attackOrigin;
	protected Vector2 attackTarget;

	protected Animator baseAnim;

	protected readonly Blocker directionBlocker = new();

	protected int holdDirection;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			baseAnim.SetBool(_weaponName, true);
			DetermineAttackPosition();
			hit.OnEnter();
		});

		exitActions.Add(() =>
		{
			baseAnim.SetBool(_weaponName, false);
			baseAnim.SetBool("mirror", false);
			hit.OnExit();
		});

		updateActions.Add(() =>
		{
			LookAtAttackPosition();
		});

		drawGizmosActions.Add(() =>
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(entity.Center, attackDistance);
			Gizmos.DrawWireSphere(attackTarget, attackRadius);
		});
	}

	public void Initialize(Animator baseAnim, Animator anim)
	{
		this.baseAnim = baseAnim;
		this.anim = anim;

		attackDistance.Set(attackDistance.Max);
		attackRadius.Set(attackRadius.Max);
		duration.Set(duration.Max);
		cooldown.Set(cooldown.Max);
	}

	public void HoldDirection(int direction)
	{
		holdDirection = direction;
		directionBlocker.AddBlock();
	}

	public void ReleaseDirection()
	{
		directionBlocker.RemoveBlock();
	}
	private void LookAtAttackPosition()
	{
		if (!directionBlocker.IsLocked)
		{
			int facingDirection = attackTarget.x > entity.Center.x ? 1 : -1;
			baseAnim.SetBool("mirror", facingDirection != entity.facingDirection);
			entity.RotateIntoDirection(facingDirection);
		}
	}

	private void DetermineAttackPosition()
	{
		attackOrigin = entity.Center;

		Vector2 attackDirection = entity.controller.lookAtDirection;

		if (entity.controller.lookAtDistance < attackDistance || attackDistance.Max == 0)
		{
			attackTarget = entity.controller.lookAtPosition;
		}
		else if (directionBlocker.IsLocked && attackDirection.x >= 0 != holdDirection >= 0)
		{
			attackTarget.x = attackOrigin.x;
		}
		else
		{
			attackTarget = attackOrigin + attackDirection * attackDistance;
		}

		RaycastHit2D hit = Utility.Check(Physics2D.Linecast, attackOrigin, attackTarget, entity.checkers.whatIsTarget);
		if (hit)
		{
			attackTarget = hit.point;
		} 
	}
}
