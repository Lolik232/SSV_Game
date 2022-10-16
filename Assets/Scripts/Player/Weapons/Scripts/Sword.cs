using System;

using UnityEngine;

public class Sword : Weapon
{
	[SerializeField] private float _maxAttackDistance;
	[SerializeField] private float _attackAngle;

	private Vector2 _attackPosition;

	[NonSerialized] public bool isFlipBlocked;

	protected override void Start()
	{
		base.Start();

		enterActions.Add(() =>
		{
			Vector2 maxAttackVector = inputReader.mouseInputDirection * _maxAttackDistance;
			Vector2 attackVector = inputReader.mouseInputPosition - player.Center;
			if (attackVector.magnitude > maxAttackVector.magnitude)
			{
				attackVector = maxAttackVector;
			}
			_attackPosition = attackVector + player.Center;

			player.CheckIfShouldFlip(attackVector.x >= 0 ? 1 : -1);
		});
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(_attackPosition, _attackAngle);
	}
}
