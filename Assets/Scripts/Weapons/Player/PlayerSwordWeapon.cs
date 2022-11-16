using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PlayerSwordWeapon : Weapon
{
    [SerializeField] private float _swordLength;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _force;
    [SerializeField] private float _damage;

    private Player _player;
    private LineRenderer _lr;

    private Coroutine _hitHolder;

    protected override void Awake()
    {
        base.Awake();
        _player = Inventory.GetComponentInParent<Player>();
        _lr = GetComponent<LineRenderer>();
    }

    protected override void Start()
    {
        SetAnimationSpeed("SwordAttack", _attackSpeed);
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();

        _player.LookAt(_player.Input.LookAt);
        _player.BlockRotation();

        collisions = new List<Collider2D>(Physics2D.OverlapCircleAll(_player.Center, _swordLength, whatIsTarget));

        foreach (var collider in collisions)
        {
            if (collider.TryGetComponent<Entity>(out var entity))
            {
                if (entity is IPhysical)
                {
                    var physical = entity as IPhysical;
                    physical.Push(_force, _player.BodyDirection * Vector2.one);
                }

                if (entity is IDamageable)
                {
                    var damageable = entity as IDamageable;
                    if (!damageable.IsDead)
                    {
                        damageable.TakeDamage(_damage, _player);
                    }
                }
            }
        }

        if (_hitHolder != null)
        {
            StopCoroutine(_hitHolder);
        }

        _hitHolder = StartCoroutine(DrawHit());
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        _player.UnlockRotation();
    }

    private IEnumerator DrawHit()
    {
        yield return new WaitUntil(() => ActiveTime > _attackSpeed);

        _player.AttackAbility.OnExit();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawCircle(_player.Center, _swordLength, collisions.Count > 0, Color.red);
    }
}
