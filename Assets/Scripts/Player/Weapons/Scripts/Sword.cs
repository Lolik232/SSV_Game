using UnityEngine;

public class Sword : Weapon
{
	[SerializeField] private float _maxAttackDistance;

	private Vector2 _attackPoint;

	protected override void Start()
	{
		base.Start();

		enterActions.Add(() =>
		{
			Vector2 maxAttackVector = Player.attackDirection * _maxAttackDistance;
			Vector2 attackVector = Player.attackPoint - Player.Center;
			if (attackVector.magnitude > maxAttackVector.magnitude)
			{
				attackVector = maxAttackVector;
			}
			transform.position = attackVector + Player.Center;
		});
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
	}
}
