using UnityEngine;

public abstract class WeaponSO : ActionComponentSO
{
	[SerializeField] private string _weaponName;

	[SerializeField] protected Parameter attackDistance;
	[SerializeField] protected Parameter attackRadius;

	[SerializeField] protected HitSO hit;

	protected Vector2 attackOrigin;
	protected Vector2 attackTarget;

	protected readonly Blocker directionBlocker = new();

	protected int holdDirection;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			DetermineAttackPosition();
			hit.OnEnter();
		});

		exitActions.Add(() =>
		{
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
			//baseAnim.SetBool("mirror", facingDirection != entity.direction.facing);
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
