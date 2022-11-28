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

    public bool IsDead
    {
        get;
    }

    public void OnDead();

    public void TakeDamage(float damage, Vector2 attackPoint);

    public void RestoreHealth(float regeneration);
}
