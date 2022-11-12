using System;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
	[SerializeField] private float _health;

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

	private void Awake()
	{
		MaxHealth = _health;
		Health = _health;
	}

	public void RestoreHealth(float regeneration)
	{
		if (regeneration < 0)
		{
			throw new Exception("Regeneration Cannot Be Negative");
		}

		Health = Mathf.Clamp(Health + regeneration, 0, MaxHealth);
		Debug.Log(this + " Health: " + Health);
	}

	public void TakeDamage(float damage)
	{
		if (damage < 0)
		{
			throw new Exception("Damage Cannot Be Negative");
		}

		Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
		Debug.Log(this + " Health: " + Health);
	}
}
