using UnityEngine;

public class Sword : Weapon
{
	[SerializeField] private float _maxAttackDistance;
	[SerializeField] private float _attackAngle;

	private Vector2 _attackPosition;

	protected override void Start()
	{
		base.Start();

		enterActions.Add(() =>
		{
			Vector2 attackVector = inputReader.mouseInputDirection * ((inputReader.mouseInputDistance > _maxAttackDistance && _maxAttackDistance != 0) ? _maxAttackDistance : inputReader.mouseInputDistance);

			if (directionBlocker.IsLocked && attackVector.x >= 0 != holdDirection >= 0)
			{
				_attackPosition = attackVector * Vector2.up + player.Center;
			}
			else
			{
				_attackPosition = attackVector + player.Center;
				if (!directionBlocker.IsLocked)
				{
					int facingDirection = attackVector.x >= 0 ? 1 : -1;
					if (facingDirection != player.facingDirection)
					{
						baseAnim.SetBool("mirror", true);
					}

					player.HoldDirection(facingDirection);
				}
			}

			HoldHitPosition(_attackPosition);
		});

		exitActions.Add(() =>
		{
			player.ReleaseDirection();
			baseAnim.SetBool("mirror", false);
		});
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(_attackPosition, _attackAngle);
	}
}
