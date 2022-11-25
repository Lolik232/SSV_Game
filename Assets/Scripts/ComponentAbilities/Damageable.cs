using System;
using All.Events;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(DeadState))]
public class Damageable : Component, IDamageable
{
    [SerializeField] private HealthEventChannelSO _didHealthChangeEventChannelSo;
    
    [FormerlySerializedAs("_health")] [SerializeField] private float _startHealth;
    private float _health;

    private StateMachine _machine;
    private DeadState _deadState;
    private Animator _anim;

    public float MaxHealth
    {
        get;
        set;
    }
    public float Health
    {
        get => _health;
        private set
        {
            _health = value;
            _didHealthChangeEventChannelSo?.RaiseEvent(_health, MaxHealth);
        }
    }
    public bool IsDead
    {
        get;
        private set;
    }

    private void Awake()
    {
        _machine = GetComponent<StateMachine>();
        _deadState = GetComponent<DeadState>();
        _anim = GetComponent<Animator>();

        MaxHealth = _startHealth;
        Health = _startHealth;
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

    public void TakeDamage(float damage, Vector2 attackPoint)
    {
        if (IsDead)
        {
            return;
        }

        if (damage < 0)
        {
            throw new Exception("Damage Cannot Be Negative");
        }

        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        Debug.Log(this + " Health: " + Health);

        if (Health == 0)
        {
            OnDead();
        }
        else
        {
            _anim.SetTrigger("hit");
        }
    }

    public void OnDead()
    {
        IsDead = true;
        _machine.GetTransition(_deadState);
    }
}
