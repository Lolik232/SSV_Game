using System;

using UnityEngine;

[RequireComponent(typeof(DeadState), typeof(StateMachine))]

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;

    private StateMachine _machine;
    private DeadState _deadState;

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
        _machine  = GetComponent<StateMachine>();
        _deadState = GetComponent<DeadState>();

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

        if (Health == 0)
        {
            _machine.GetTransition(_deadState);
        }
    }
}
