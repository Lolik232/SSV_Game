using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSword", menuName = "Weapons/Weapon/Sword/Player")]
public class PlayerSwordSO : PlayerWeaponSO
{
	private Vector2 _attackPosition;

	protected override void OnEnable()
	{
		base.OnEnable();

		enterActions.Add(() =>
		{
			Vector2 attackVector = data.input.mouseInputDirection * ((data.input.mouseInputDistance > data.parameters.swordAttackDistance && data.parameters.swordAttackDistance.Max != 0) ? data.parameters.swordAttackDistance : data.input.mouseInputDistance);

			if (directionBlocker.IsLocked && attackVector.x >= 0 != holdDirection >= 0)
			{
				_attackPosition = attackVector * Vector2.up + entity.Center;
			}
			else
			{
				_attackPosition = attackVector + entity.Center;
				if (!directionBlocker.IsLocked)
				{
					int facingDirection = attackVector.x >= 0 ? 1 : -1;
					if (facingDirection != entity.facingDirection)
					{
						baseAnim.SetBool("mirror", true);
					}

					entity.HoldDirection(facingDirection);
				}
			}

			hit.OnHit(_attackPosition);
		});

		exitActions.Add(() =>
		{
			entity.ReleaseDirection();
			baseAnim.SetBool("mirror", false);
		});
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(entity.Center, data.parameters.swordAttackDistance);
		Gizmos.DrawWireSphere(_attackPosition, data.parameters.swordAttackAngle);
	}
}
