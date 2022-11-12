using UnityEngine;

public interface IDamageable
{
	public float MaxHealth
	{
		get;
		set;
	}

	public float Health
	{
		get;
	}

	public void TakeDamage(float damage);

	public void RestoreHealth(float regeneration);
}
