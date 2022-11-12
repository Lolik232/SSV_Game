using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public float MaxHealth
	{
		get;
		set;
	}
	public float Health
	{
		get;
		private set;
	}

	public void RestoreHealth(float regeneration)
	{
		throw new System.NotImplementedException();
	}

	public void TakeDamage(float damage)
	{
		throw new System.NotImplementedException();
	}
}
