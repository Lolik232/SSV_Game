using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerSwordWeapon : PlayerWeapon
{
    [SerializeField] private float _swordLength;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _force;
    [SerializeField] private float _damage;

    private Coroutine _hitHolder;

    protected override void Start()
    {
        SetAnimationSpeed("SwordAttack", _attackSpeed);
    }

    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();

        Player.LookAt(Player.Input.LookAt);
        Player.BlockRotation();

        collisions = new List<Collider2D>(Physics2D.OverlapCircleAll(Player.Center, _swordLength, whatIsTarget));

        foreach (var collider in collisions)
        {
            if (collider.TryGetComponent<Entity>(out var entity))
            {
                if (entity is IPhysical)
                {
                    var physical = entity as IPhysical;
                    physical.Push(_force, Player.BodyDirection * Vector2.one);
                }

                if (entity is IDamageable)
                {
                    var damageable = entity as IDamageable;
                    if (!damageable.IsDead)
                    {
                        damageable.TakeDamage(_damage, Player);
                    }
                }
            }
        }

        if (_hitHolder != null)
        {
            StopCoroutine(_hitHolder);
        }

        _hitHolder = StartCoroutine(OnHit());
    }

    protected override void ApplyExitActions()
    {
        base.ApplyExitActions();
        Player.UnlockRotation();
    }

    private IEnumerator OnHit()
    {
        yield return new WaitUntil(() => ActiveTime > _attackSpeed);

        Player.AttackAbility.OnExit();
    }

    private void OnDrawGizmos()
    {
        Utility.DrawCircle(Player.Center, _swordLength, collisions.Count > 0, Color.red);
    }
}
