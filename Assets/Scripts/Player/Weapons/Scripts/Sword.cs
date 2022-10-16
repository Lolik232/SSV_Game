using System;

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

			if (isDirectionHoldOn && attackVector.x >= 0 != heldDirection >= 0)
			{
				_attackPosition = attackVector * Vector2.up + player.Center;
			}
			else
			{
				_attackPosition = attackVector + player.Center;
				if (!isDirectionHoldOn)
				{
					player.CheckIfShouldFlip(attackVector.x >= 0 ? 1 : -1);
				}
			}

			hitPos.position = _attackPosition;
			hitSr.enabled = true;
		});

		exitActions.Add(() =>
		{
			hitSr.enabled = false;
		});
	}



	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(_attackPosition, _attackAngle);
	}
}
