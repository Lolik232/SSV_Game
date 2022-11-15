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

    public void TakeDamage(float damage, Entity damager);

    public void RestoreHealth(float regeneration);
}
